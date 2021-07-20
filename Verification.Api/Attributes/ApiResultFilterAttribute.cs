using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Verification.Api.Data;

namespace Verification.Api.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ApiResultFilterAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            var objectResult = context.Result as ObjectResult;
            context.Result = new OkObjectResult(new BaseResultModel(objectResult?.StatusCode,
                objectResult?.Value,
                objectResult?.StatusCode != 200 ? $"{objectResult?.Value}" : "Successful"));
        }
    }
}