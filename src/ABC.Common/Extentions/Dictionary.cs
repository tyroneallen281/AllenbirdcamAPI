using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ABC.Common.Extentions
{
    public static class Dictionary
    {
        public static Dictionary<string, string> ToDictionary(this object obj)
        {
            try
            {
                var json = JsonConvert.SerializeObject(obj);
                var dictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
                return dictionary;
            }
            catch (Exception ex)
            {
                return new Dictionary<string, string>();
            }
        
        }
    }
}
