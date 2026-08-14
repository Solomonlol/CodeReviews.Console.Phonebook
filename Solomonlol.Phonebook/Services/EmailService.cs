using Backend.Interfaces;
using Backend.Models;
using Backend.Models.Dto;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace Backend.Services
{
    public class EmailService : IMessage
    {
        private readonly CurrentUserService _currentUserService;
        private readonly EmailPasswordProtection _passwordProtection;
        private readonly SmtpSettings _smtpSettings;
        public EmailService(EmailPasswordProtection passwordProtection, IOptions<SmtpSettings> options)
        {
            _passwordProtection = passwordProtection;
            _smtpSettings = options.Value;
        }

        public async Task SendMessageAsync(string header, string message, User sender, ContactDto receiver)
        {
            var password = _passwordProtection.Unprotect(sender.EmailPasswordProtected);
            
            var from = new MailAddress($"{sender.Email}", $"{sender.FirstName} {sender.LastName}");
            var to = new MailAddress($"{receiver.Email}");
            using var m = new MailMessage(from, to)
            {
                Subject = $"{header}",
                Body = $"{message}"
            };

            using var smtp = new SmtpClient(_smtpSettings.Host, _smtpSettings.Port)
            {
                Credentials = new NetworkCredential($"{sender.Email}", $"{password}"),
                EnableSsl = _smtpSettings.EnableSsl,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = _smtpSettings.UseDefaultCredentials
            };

            await smtp.SendMailAsync(m);
            Console.WriteLine("The email has been successfully sent.");
        }
    }
}
