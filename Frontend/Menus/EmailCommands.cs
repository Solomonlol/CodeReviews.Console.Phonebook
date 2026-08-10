using Backend.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Frontend.Menus
{
    internal class EmailCommands
    {
        private readonly EmailService _emailService;
        public EmailCommands(EmailService service)
        {
            _emailService = service;
        }

        public async Task Send()
        {

        }
    }
}
