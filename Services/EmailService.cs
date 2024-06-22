using Entities;
using MailKit.Net.Smtp;
using MimeKit;
using MailKit.Security;
using ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class EmailService : IEmailService
    {
        private Timer _timer;
        private readonly EmailConfiguration _emailConfig;
        public EmailService(EmailConfiguration emailConfig)
        {
            _emailConfig = emailConfig;
            _timer = null;
        }
        public async Task SendEmailAsync(Message message)
        {
            var emailMessage = CreateEmailMessage(message);
            await SendAsync(emailMessage);
        }

        private async Task SendAsync(MimeMessage emailMessage)
        {
            using var client = new SmtpClient();
            try
            {
                await client.ConnectAsync(_emailConfig.SmtpServer, _emailConfig.Port, true);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                await client.AuthenticateAsync(_emailConfig.UserName, _emailConfig.Password);

                await client.SendAsync(emailMessage);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                await client.DisconnectAsync(true);
                client.Dispose();
            }
        }

        private MimeMessage CreateEmailMessage(Message message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("email", _emailConfig.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;

            var bodyBuidler = new BodyBuilder
            {
                HtmlBody = message.Content
            };


            emailMessage.Body = bodyBuidler.ToMessageBody();
            //emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text)
            //{
            //    Text = message.Content
            //};
            return emailMessage;
        }
        public async Task ScheduleOneTimeEmail( Message emailMessage, DateTime SendTime)
        {
            var dueTime = SendTime - DateTime.Now;
            if (dueTime.TotalMilliseconds > 0)
            {
                _timer = new Timer(SendEmailCallback, null, dueTime, Timeout.InfiniteTimeSpan);
            }
            else
            {
                // Schedule the email for immediate delivery if the scheduled time has already passed
                 SendEmailAsync(emailMessage).Wait();
            }

            void SendEmailCallback(object state)
            {
                 SendEmailAsync(emailMessage).Wait();
            }
        }
    }
}
