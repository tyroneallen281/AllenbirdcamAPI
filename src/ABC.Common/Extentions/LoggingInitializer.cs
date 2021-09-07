using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;
using System;
using System.Collections.Generic;
using System.Text;

namespace ABC.Common.Extentions
{
     public class LoggingInitializer : ITelemetryInitializer
     {
         readonly string roleName;
         public LoggingInitializer(string roleName = null)
         {
             this.roleName = roleName ?? "api";
         }
         public void Initialize(ITelemetry telemetry)
         {
             telemetry.Context.Cloud.RoleName = roleName;
         }
     }
    
}
