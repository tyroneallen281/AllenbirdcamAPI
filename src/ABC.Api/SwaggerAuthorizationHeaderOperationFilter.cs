﻿using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;


namespace ABC.Api
{
    //public class SwaggerAuthorizationHeaderOperationFilter : IOperationFilter
    //{
    //    public void Apply(Operation operation, OperationFilterContext context)
    //    {
    //        //IList<FilterDescriptor> filterDescriptors = context.ApiDescription.ActionDescriptor.FilterDescriptors;
    //        //bool isAuthorized = filterDescriptors.Select(filterInfo => filterInfo.Filter).Any(filter => filter is AuthorizeFilter);
    //        //bool allowAnonymous = filterDescriptors.Select(filterInfo => filterInfo.Filter).Any(filter => filter is IAllowAnonymousFilter);

    //        //if (isAuthorized && !allowAnonymous)
    //        //{
    //        //    if (operation.Parameters == null)
    //        //    {
    //        //        operation.Parameters = new List<IParameter>();
    //        //    }

    //        //    operation.Parameters.Add(new NonBodyParameter
    //        //    {
    //        //        Name = "Authorization",
    //        //        In = "header",
    //        //        Description = "Access token",
    //        //        Required = true,
    //        //        Type = "string",
    //        //        Default = "Bearer "
    //        //    });
    //        //}
    //    }
    //}
}
