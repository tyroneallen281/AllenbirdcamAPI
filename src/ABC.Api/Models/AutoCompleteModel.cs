using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC.Api.Models
{
    public class AutoCompeteModel
    {
        public string value { get; set; }
        public AutoCompeteDataModel data { get; set; }
    }

    public class AutoCompeteDataModel
    {
        public string subProp1 { get; set; }
        public string subProp2 { get; set; }
        public string value { get; set; }
        public string category { get; set; }
        public string id { get; set; }
    }
  
}
