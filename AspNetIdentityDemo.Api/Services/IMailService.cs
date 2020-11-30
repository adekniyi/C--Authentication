using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetIdentityDemo.Api.Services
{
    public interface IMailService
    {
        Task SendMailAsync(string toEmail, string subject, string content);
    }

    public class MailService : IMailService
    {
        private IConfiguration _configuration;
        public MailService(IConfiguration configuration)
         {
            _configuration = configuration;
         }

        public async  Task SendMailAsync(string toEmail, string subject, string content)
        {
            var apiKey = _configuration["SendGridAPIKey"];
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("adekunle.adeniyi@quomodosystems.com", "Authentication Demo");
            var to = new EmailAddress(toEmail);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, content, content);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
