using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Verification.Filters
{
    public class CustomExceptionFilter : IExceptionFilter
    {
        private ILogger _logger;

        public CustomExceptionFilter(ILogger logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            var status = HttpStatusCode.InternalServerError;
            var message = context.Exception.Message;

            // log 
            _logger.LogError(context.Exception, $"Ex massage: {message}, StackTrace: {context.Exception.StackTrace}",
                context.Exception);
            // exception has been finished.
            context.ExceptionHandled = true;
            var response = context.HttpContext.Response;
            response.StatusCode = (int) status;
            response.ContentType = "application/json";

            context.Result = new ObjectResult(new
                {Timestamp = DateTimeOffset.UtcNow, Message = message, Result = "Ex_filter Error"});
        }
    }
}