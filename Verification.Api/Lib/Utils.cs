using System;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Verification.Api.Lib
{
    public static class Utils
    {
        public static string GetValueByKey (ActionExecutingContext context, string parameters ,string target)
        {
            var val = string.Empty;
            var param = context.ActionArguments[parameters];
            var props = param.GetType().GetProperties();
            foreach (var item in props)
            {
                var key = item.Name;
                if (key.Equals(target, StringComparison.OrdinalIgnoreCase))
                {
                    val = param.GetType().GetProperty(target)?.GetValue(param, null)?.ToString();
                    return val;
                }
            }
            return val;
        }
    }
}