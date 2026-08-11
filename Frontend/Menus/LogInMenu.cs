using Backend.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Frontend.Menus
{
    internal class LogInMenu : UserInterface
    {
        private readonly EmailMenu _emailMenu;
        private readonly ContactManagementMenu _contactManagementMenu;

        public LogInMenu(EmailMenu emailMenu,
                        ContactManagementMenu contactManagementMenu) : base("Log In menu")
        {
            _emailMenu = emailMenu;
            
            _contactManagementMenu = contactManagementMenu;
            AddItem("Contact management", () => ContactManagement());
            AddItem("Send message", () => EmailMenu());
            AddExitItem("Back");
        }

        public async Task ContactManagement()
        {
            await _contactManagementMenu.RunAsync();
        }

        public async Task EmailMenu()
        {
            await _emailMenu.RunAsync();
        }
    }
}
