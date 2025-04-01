using HR.LeaveManagement.Application.Contracts.Infrastructure;
using HR.LeaveManagement.Application.Models;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;
using System;
using Microsoft.Extensions.Logging;

namespace HR.LeaveManagement.Infrastructure.Mail
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailSetting _emailConfig;
        private readonly ILogger<EmailSender> logger;

        public EmailSender(IOptionsMonitor<EmailSetting> options, ILogger<EmailSender> logger)
        {
            _emailConfig = options.CurrentValue;
            this.logger = logger;
        }
        public async Task<bool> SendEmailAsync(Email email)
        {

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_emailConfig.FromName, _emailConfig.FromAddress));
            message.To.Add(new MailboxAddress(string.Empty, email.To));
            message.Subject = email.Subject;

            var bodyBuilder = new BodyBuilder();
            if (email.IsHtml)
                bodyBuilder.HtmlBody = email.Body;
            else
                bodyBuilder.TextBody = email.Body;

            message.Body = bodyBuilder.ToMessageBody();

            try
            {
                using (var client = new SmtpClient())
                {
                    if (_emailConfig.IsTcp)
                        client.Connect(_emailConfig.SmtpServer, _emailConfig.SmtpPort, MailKit.Security.SecureSocketOptions.StartTls);
                    else
                        client.Connect(_emailConfig.SmtpServer, _emailConfig.SmtpPort);

                    client.Authenticate(_emailConfig.FromAddress, _emailConfig.Password);
                    client.Send(message);

                    logger.LogInformation($"Email sent successfully to {email.To}");

                    client.Disconnect(true);
                }
                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to send mail to {EmailAddress}.", email.To);
                return false;
            }

        }
    }
}
