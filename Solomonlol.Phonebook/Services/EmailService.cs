using Backend.Interfaces;
using Backend.Models;
using Backend.Models.Dto;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Backend.Services
{
    public class EmailService : IMessage
    {
        private readonly CurrentUserService _currentUserService;
        private readonly EmailPasswordProtection _passwordProtection;
        public EmailService(CurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
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

            using var smtp = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential($"{sender.Email}", $"{password}"),
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false
            };

            await smtp.SendMailAsync(m);
            Console.WriteLine("Письмо отправлено");
        }
    }
}
