using ABC.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ABC.Api.Models
{
  
    public class VotePostModel
    {
        public int SightingId { get; set; }
        public VoteEnum Vote { get; set; }
        public string IPAddress { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}