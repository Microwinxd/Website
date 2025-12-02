using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;

namespace BeanScene1._1.Services
{
    public class SmtpEmailSender : IEmailSender
    {
        private readonly IConfiguration _config;
        public SmtpEmailSender(IConfiguration config)
        {
            _config = config;
        }
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)  
        {
            var smtpClient = new SmtpClient(_config["Smtp:HOST"])
            {
                Port = int.Parse(_config["Smtp:PORT"]),
                Credentials = new NetworkCredential(_config["Smtp:User"], _config["Smtp:Password"]),
                EnableSsl = true,
            };
            var mailMessage = new MailMessage
            {
                From = new MailAddress(_config["Smtp:User"], "BeanScene account confirmation"),
                Subject = subject,
                Body = htmlMessage,
                IsBodyHtml = true

            };
            mailMessage.To.Add(email);
            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}
