using Backend.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Frontend.Menus
{
    internal class ContactMenu : UserInterface
    {
        private readonly ContactService _contactService;

        public ContactMenu(ContactService service) : base("Contact menu")
        {
            _contactService = service;
            AddItem("Create user", () => Create());
            AddItem("Update user", () => Update());
            AddItem("Delete user", () => Delete());
            AddExitItem("Back");
        }

        public Task Create(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task Delete(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task Update(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task Back()
        {
            await Task.CompletedTask;
        }


    }
}
