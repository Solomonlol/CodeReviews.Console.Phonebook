using Backend.Models.Dto;
using Backend.Services;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace Frontend.Commands
{
    internal class UserCommands
    {
        private readonly UserService _userService;
        private readonly Dictionary<string, Func<Task>> _userMenu;
        public UserCommands(UserService service)
        {
            _userService = service;
            _userMenu = new()
            {
                { "Back", () => Task.CompletedTask },
                { "Create user", ()=>CreateUser() }
            };
        }

        public async Task CreateUser(CancellationToken cancellationToken = default)
        {
            CreateUserDto dto = new CreateUserDto
            {
                Login = AnsiConsole.Ask<string>("Login:"),
                Password = AnsiConsole.Prompt(
                                    new TextPrompt<string>("Password:")
                                    .Secret('*')),
                PhoneNumber = AnsiConsole.Prompt(
                                    new TextPrompt<string>("Phone number:")),
                FirstName = AnsiConsole.Ask<string>("First name:"),
                LastName = AnsiConsole.Prompt(
                                    new TextPrompt<string>("Last name (optional):")
                                    .AllowEmpty()),
                MiddleName = AnsiConsole.Prompt(
                                    new TextPrompt<string>("Middle name (optional):")
                                    .AllowEmpty()),
                Email = AnsiConsole.Prompt(
                                    new TextPrompt<string>("Email (optional):")
                                    .AllowEmpty()),
            };
            await _userService.CreateAsync(dto, cancellationToken);
        }

        public async Task DeleteUser(CancellationToken cancellationToken = default)
        {

        }

        public async Task UpdateUser(CancellationToken cancellationToken = default)
        {

        }

        //public async Task DeleteUser(CancellationToken cancellationToken = default)
        //{

        //}

        public Dictionary<string, Func<Task>> GetSubMenu()
        {
            return _userMenu;
        }

    }
}
