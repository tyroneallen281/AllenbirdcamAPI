using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ABC.Api.Models
{
    public enum OrderDirection
    {
        Asc, Desc
    }

    public class FilterParameters
    {
        public string Query { get; set; }
        public string OrderColumn { get; set; }
        public OrderDirection OrderDirection { get; set; }
        public int? Skip { get; set; }

        public int? Top { get; set; }
     
    }
}