using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Verification.EmailSender;
using Verification.Services;

namespace Verification.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class GenerateVCodeAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int _timeToLiveSeconds;

        public GenerateVCodeAttribute(int timeToLiveSeconds)
        {
            _timeToLiveSeconds = timeToLiveSeconds;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cacheSettings = context.HttpContext.RequestServices.GetRequiredService<RedisCacheSettings>();
            if (!cacheSettings.Enabled)
            {
                await next();
                return;
            }

            var email = GetValueByKey(context, "Email");
            var executedContext = await next();
            if (executedContext.Result is OkObjectResult)
            {
                var rd = new Random();
                var vCode = rd.Next(10000, 100000).ToString();
                var cacheService = context.HttpContext.RequestServices.GetRequiredService<IResponseCacheService>();
                await cacheService.CacheResponseAsync($"VCode_{email}", vCode,
                    TimeSpan.FromSeconds(_timeToLiveSeconds));

                // notice email sender to work
                await Sender.Run(email, vCode);
            }
        }

        private string GetValueByKey(ActionExecutingContext context, string target)
        {
            var email = string.Empty;
            var param = context.ActionArguments["parameters"];
            var props = param.GetType().GetProperties();
            foreach (var item in props)
            {
                var key = item.Name;
                if (key.Equals(target, StringComparison.OrdinalIgnoreCase))
                {
                    email = param.GetType().GetProperty(target)?.GetValue(param, null)?.ToString();
                }
            }

            return email;
        }
    }
}