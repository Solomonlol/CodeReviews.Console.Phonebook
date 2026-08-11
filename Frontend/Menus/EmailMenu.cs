using Backend.Models.Dto;
using Backend.Services;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace Frontend.Menus
{
    internal class EmailMenu : UserInterface
    {
        private readonly EmailService _emailService;
        private readonly ContactService _contactService;
        private readonly CurrentUserService _currentUserService;

        public EmailMenu(EmailService service, CurrentUserService currentUserService, ContactService contactService) : base("User management")
        {
            _emailService = service;
            _currentUserService = currentUserService;
            _contactService = contactService;
        }

        public async Task Send()
        {
            var contactList = (await _contactService.GetList()).ToList();
            var contactChoise = await AnsiConsole.PromptAsync(new MultiSelectionPrompt<ContactDto>()
                                                        .Title("Choose who to send the message to:")
                                                        .UseConverter(c => $"{c.FirstName} | {c.PhoneNumber} | {c.Email}")
                                                        .AddChoices(contactList));
            var header = await AnsiConsole.AskAsync<string>("Enter [green]header[/]:");
            var message = await AnsiConsole.AskAsync<string>("Enter your [green]message[/]");
            foreach (var con in contactChoise)
            {
                await _emailService.SendMessageAsync(header, message, _currentUserService.CurrentUser, con);
            }
        }
    }
}
