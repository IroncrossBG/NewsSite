namespace NewsSite.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity.UI.Services;
    using Microsoft.Extensions.Configuration;
    using SendGrid;
    using SendGrid.Helpers.Mail;

    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration configuration;

        public EmailSender(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var apiKey = configuration.GetSection("APIKeys").GetSection("SendGrid").Value;
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("bg-novini@abv.bg", "BG Novini");
            var to = new EmailAddress(email);
            var htmlContent = htmlMessage;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, "BG Novini", htmlContent);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
