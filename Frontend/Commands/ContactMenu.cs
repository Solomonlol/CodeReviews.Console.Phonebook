using Backend.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Frontend.Commands
{
    internal class ContactMenu
    {
        private readonly ContactService _contactService;
        public ContactMenu(ContactService service)
        {
            _contactService = service;
        }
    }
}
