using System;
using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;
using MailKit.Net.Smtp;
using MimeKit;


namespace Verification.EmailSender
{
    public static class Sender
    {
        private static string _email;
        private static string _vCode;

        public static async Task Run(string email, string vCode)
        {
            _email = email;
            _vCode = vCode;
            ISchedulerFactory schedf = new StdSchedulerFactory();
            var sched = await schedf.GetScheduler();
            await sched.Start();

            var trigger = TriggerBuilder.Create()
                .WithIdentity("trigger1", "group1")
                .StartNow()
                .Build();

            var job = JobBuilder.Create<MessageJob>().Build();
            sched.ScheduleJob(job, trigger).Wait();
        }

        private class MessageJob : IJob
        {
            public Task Execute(IJobExecutionContext context)
            {
                return Task.Run(() => { SendEmail(); });
            }
        }

        public static void SendEmail()
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Sam", "sam.test@test.com"));
            message.To.Add(new MailboxAddress(_email, _email));
            message.Subject = "Verification Code (Test)";

            message.Body = new TextPart("plain")
            {
                Text = $@"Your Verification Code is [{_vCode}], and it will expire after 30 minutes!"
            };

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 465, true);
                client.Authenticate("", "");
                client.Send(message);
                client.Disconnect(true);
            }
            Console.WriteLine("the email has been sent");
        }
    }
}