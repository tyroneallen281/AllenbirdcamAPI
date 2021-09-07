using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace ABC.Domain.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string source)
        {
            return string.IsNullOrEmpty(source);
        }

        public static string ToTitleCase(this string source)
        {
            return source.IsNullOrEmpty() ? source : CultureInfo.InvariantCulture.TextInfo.ToTitleCase(source);
        }

        public static string Concatenate(this IEnumerable<string> source, string delimeter)
        {
            return source.Any() ? source.Aggregate((x, y) => x + delimeter + y) : string.Empty;
        }
    }
}
