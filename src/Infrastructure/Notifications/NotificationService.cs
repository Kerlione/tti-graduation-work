using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using tti_graduation_work.Application.Common.Interfaces;
using tti_graduation_work.Infrastructure.Email;

namespace tti_graduation_work.Infrastructure.Notifications
{
    public class NotificationService : INotificationService
    {
        private readonly EmailConfiguration _emailConfiguration;
        private readonly ILogger _logger;

        public NotificationService(EmailConfiguration emailConfiguration, ILogger<NotificationService> logger)
        {
            _emailConfiguration = emailConfiguration;
            _logger = logger;
        }
        public void Notify(INotificationModel notification)
        {
            var emailMessage = CreateEmailMessage(notification);

            Send(emailMessage);
        }

        private MimeMessage CreateEmailMessage(INotificationModel message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_emailConfiguration.From));
            emailMessage.To.Add(new MailboxAddress(message.Recepient));
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = message.Content };

            return emailMessage;
        }

        private void Send(MimeMessage mailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect(_emailConfiguration.SmtpServer, _emailConfiguration.Port, true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(_emailConfiguration.UserName, _emailConfiguration.Password);

                    client.Send(mailMessage);
                }
                catch(Exception ex)
                {
                    _logger.LogError($"Message delivery failed", ex);
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }
    }
}
