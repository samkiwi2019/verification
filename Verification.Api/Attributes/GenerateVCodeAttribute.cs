using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Verification.Api.Services;
using MailKit.Net.Smtp;
using MimeKit;
using Verification.Api.Lib;

namespace Verification.Api.Attributes
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
            var email = Utils.GetValueByKey(context, "parameters","Email");

            if (executedContext.Result is OkObjectResult && email is not null)
            {
                var rd = new Random();
                var vCode = rd.Next(10000, 100000);
                var cacheService = context.HttpContext.RequestServices.GetRequiredService<IResponseCacheService>();
                await cacheService.CacheResponseAsync($"VCode_{email}", vCode,
                    TimeSpan.FromSeconds(_timeToLiveSeconds));

                // notice email sender to work
                await Send(email, vCode.ToString());
            }
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
                    Console.WriteLine("sending an email successful");
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