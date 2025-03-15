using HR.LeaveManagement.Application.Contracts.Infrastructure;
using HR.LeaveManagement.Application.Models;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;
using System;

namespace HR.LeaveManagement.Infrastructure.Mail
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailSetting _emailConfig;

        public EmailSender(IOptionsMonitor<EmailSetting> options)
        {
            _emailConfig = options.CurrentValue;
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
                    client.Connect(_emailConfig.SmtpServer, _emailConfig.SmtpPort, MailKit.Security.SecureSocketOptions.StartTls);
                    client.Authenticate(_emailConfig.FromAddress, _emailConfig.Password);
                    client.Send(message);
                    Console.WriteLine("Email sent successfully!");
                    client.Disconnect(true);
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }

        }
    }
}
