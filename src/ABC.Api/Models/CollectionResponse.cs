using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABC.Api.Models
{
    public class CollectionResponse<T>
    {
        public IEnumerable<T> Items { get; set; }

        public string Next { get; set; }

        public string Previous { get; set; }

        public int TotalCount { get; set; }

        public string Query { get; set; }
    }
}
