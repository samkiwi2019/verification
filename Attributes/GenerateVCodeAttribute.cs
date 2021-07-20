using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Verification.Services;
using MailKit.Net.Smtp;
using MimeKit;

namespace Verification.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public sealed class GenerateVCodeAttribute : Attribute, IAsyncActionFilter
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
            // wait for controllers 
            var executedContext = await next();
            // controllers have finished and ready for response
            var email = GetValueByKey(context, "Email");
            if (executedContext.Result is OkObjectResult && email is not null)
            {
                var rd = new Random();
                var vCode = rd.Next(10000, 100000).ToString();
                var cacheService = context.HttpContext.RequestServices.GetRequiredService<IResponseCacheService>();
                await cacheService.CacheResponseAsync($"VCode_{email}", vCode,
                    TimeSpan.FromSeconds(_timeToLiveSeconds));

                // notice email sender to work
                await Send(email, vCode);
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

        private Task Send(string email, string vCode)
        {
            try
            {
                return Task.Run(() =>
                {
                    var message = new MimeMessage();
                    message.From.Add(new MailboxAddress("Sam", "sam.vc.info@gmail.com"));
                    message.To.Add(new MailboxAddress(email, email));
                    message.Subject = "Verification Code";

                    message.Body = new TextPart("plain")
                    {
                        Text = $@"Your Verification Code is [{vCode}], and it will expire after 30 minutes!"
                    };

                    using (var client = new SmtpClient())
                    {
                        client.Connect("smtp.gmail.com", 465, true);
                        client.Authenticate("sam.vc.info@gmail.com", "samsamsam123");
                        client.Send(message);
                        client.Disconnect(true);
                    }
                    Console.WriteLine("sending successful");
                });

            }
            catch (Exception ex)
            {
                var message = $"sending a email failed. {email} - {ex.Message}";
                throw new ApplicationException(message);
            }
        }
    }
}