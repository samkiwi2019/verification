using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Verification.Data;

namespace Verification.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ApiResultFilterAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            var objectResult = context.Result as ObjectResult;
            context.Result = new OkObjectResult(new BaseResultModel(objectResult?.StatusCode,
                objectResult?.StatusCode == 200 ? objectResult.Value : null,
                objectResult?.StatusCode != 200 ? $"{objectResult?.Value}" : "Successful"));
        }
    }
}