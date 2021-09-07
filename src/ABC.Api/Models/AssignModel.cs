using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ABC.Api.Models
{
  

    public class AssignModel
    {
        public Guid UserId { get; set; }
        public List<int> AssignIds { get; set; }
   
       
    }
}