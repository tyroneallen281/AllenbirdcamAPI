using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ABC.Common.Extentions
{
    public static class EnumExtentions
    {
        public static TAttribute GetAttribute<TAttribute>(this Enum value)
       where TAttribute : Attribute
        {
            var enumType = value.GetType();
            var name = Enum.GetName(enumType, value);
            return enumType.GetField(name).GetCustomAttributes(false).OfType<TAttribute>().SingleOrDefault();
        }

    }

    public class AgeRangeAttribute : Attribute
    {
        public int Min { get; private set; }
        public int Max { get; private set; }

        public AgeRangeAttribute(int min, int max)
        {
            this.Min = min;
            this.Max = max;
        }
    }
}
