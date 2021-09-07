using Microsoft.AspNetCore.Http;
using ABC.Domain.Models;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ABC.DomainService.Interfaces;
using Serilog;

namespace ABC.DomainService.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context /* other dependencies */)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError; // 500 if unexpected

            var response = new ApiErrorResponse
            {
                ErrorDetail = $"{exception.Message} InnerException: {exception.InnerException}"
            };
            Log.Error($"HandleExceptionAsync - {context.Request.Query}", exception);
            response.ErrorMessage = "An unkown error has occured. Please contact support";
            response.ErrorType = exception.GetType().ToString();

            var result = JsonConvert.SerializeObject(response);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }
}
