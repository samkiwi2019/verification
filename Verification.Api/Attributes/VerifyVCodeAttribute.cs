using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Verification.Api.Services;
using Verification.Api.Lib;

namespace Verification.Api.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class VerifyVCodeAttribute : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cacheSettings = context.HttpContext.RequestServices.GetRequiredService<RedisCacheSettings>();
            if (!cacheSettings.Enabled)
            {
                await next();
                return;
            }

            var email = Utils.GetValueByKey(context, "userCreateDto","Email");
            var vCode = Utils.GetValueByKey(context, "userCreateDto","VCode");

            var cacheService = context.HttpContext.RequestServices.GetRequiredService<IResponseCacheService>();
            var cachedResponse = await cacheService.GetCachedResponseAsync($"VCode_{email}");

            if (string.IsNullOrEmpty(cachedResponse) || !cachedResponse.Equals(vCode))
            {
                context.Result = new BadRequestObjectResult("The vCode is invalid");
                return;
            }

            var executedContext = await next();

            // delete vcode by email
            if (executedContext.Result is OkObjectResult)
            {
                await cacheService.RemoveCacheByKey($"VCode_{email}");
            }
        }
    }
}