using Backend.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Frontend.Commands
{
    internal class ContactCommands
    {
        private readonly ContactService _contactService;
        public ContactCommands(ContactService service)
        {
            _contactService = service;
        }
    }
}
