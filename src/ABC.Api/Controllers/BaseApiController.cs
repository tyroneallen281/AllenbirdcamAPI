using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Castle.Core.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ABC.Api.Models;
using Microsoft.ApplicationInsights;
using System.Security.Claims;
using Microsoft.ApplicationInsights.Extensibility;
using ABC.Common.Extentions;
using ABC.DomainService.Interfaces;

namespace ABC.Api.Controllers
{
    [ApiController]
    public class BaseApiController : ControllerBase
    {
       // public readonly ILoggingService _logging;
        //private Alcazar.AuthWrapper.Auth _authWrapper = null;
        protected ActionResult OkFilteredCollection<T>(IEnumerable<T> collection,int totalRecords, FilterParameters filterParams, HttpRequest request) => Ok(FilteredCollection(collection, totalRecords, filterParams, request));
        
        public BaseApiController()
        {
            //_logging = null;
            var authorizedUserId = this.User?.FindFirstValue(ClaimTypes.NameIdentifier);
           
        }
        protected static CollectionResponse<T> FilteredCollection<T>(IEnumerable<T> collection, int totalRecords, FilterParameters filterParams, HttpRequest request)
        {
            var result = new CollectionResponse<T>();

            var returnValue = collection.AsEnumerable();
            result.TotalCount = totalRecords;
            result.Query = filterParams.Query;

            if (result.TotalCount == 0)
            {
                return result;
            }

            result.Items = returnValue;
            return result;
        }
    
        protected void LogEvent(string @event,object data)
        {
            //_logging.LogEvent(@event, data.ToDictionary());
        }
        protected void LogEvent(string @event, Dictionary<string,string> data)
        {
           // _logging.LogEvent(@event, data);
        }
    }
}