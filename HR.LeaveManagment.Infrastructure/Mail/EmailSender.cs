using HR.LeaveManagement.Application.Contracts;
using HR.LeaveManagement.Application.Models;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

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
            var client = new SendGridClient(_emailConfig.ApiKey);
            var to = new EmailAddress(email.To);
            var from = new EmailAddress
            {
                Email = _emailConfig.FromAddress,
                Name = _emailConfig.FromName
            };

           var message =  MailHelper.CreateSingleEmail(from, to, email.Subject, email.Body, email.Body);

          var response =   await client.SendEmailAsync(message);
           return response.StatusCode == System.Net.HttpStatusCode.OK || response.StatusCode == System.Net.HttpStatusCode.Accepted;

        }
    }
}
