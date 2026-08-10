using Backend.Models.Dto;
using Backend.Services;
using Spectre.Console;
using Backend.Validation;
using Backend.Exceptions;

namespace Frontend.Commands
{
    internal class UserMenu
    {
        private readonly UserService _userService;
        private readonly Dictionary<string, Func<Task>> _userMenu;
        public UserMenu(UserService service)
        {
            _userService = service;
            _userMenu = new()
            {
                { "Back", () => Task.CompletedTask },
                { "Create user", ()=>CreateUser() },
                { "Delete user", ()=>DeleteUser() }
            };
        }

        public async Task CreateUser(CancellationToken cancellationToken = default)
        {
            try
            {
                CreateUserDto dto = new CreateUserDto
                {
                    Login = AnsiConsole.Ask<string>("Login:"),
                    Password = AnsiConsole.Prompt(
                                        new TextPrompt<string>("Password:")
                                        .Secret('*')),
                    PhoneNumber = AnsiConsole.Prompt(
                                        new TextPrompt<string>("Phone number:")
                                        .Validate(input =>
                                        {
                                            return Validation.IsPhoneNumber(input)
                                            ? ValidationResult.Success()
                                            : ValidationResult.Error("[red]Invalid input[/]");
                                        })),
                    FirstName = AnsiConsole.Ask<string>("First name:"),
                    LastName = AnsiConsole.Prompt(
                                        new TextPrompt<string>("Last name (optional):")
                                        .AllowEmpty()),
                    MiddleName = AnsiConsole.Prompt(
                                        new TextPrompt<string>("Middle name (optional):")
                                        .AllowEmpty()),

                    Email = AnsiConsole.Prompt(
                                        new TextPrompt<string>("Email (optional):")
                                        .AllowEmpty()
                                        .Validate(input =>
                                        {
                                            if (string.IsNullOrEmpty(input))
                                                return ValidationResult.Success();

                                            return Validation.IsEmailAddress(input)
                                            ? ValidationResult.Success()
                                            : ValidationResult.Error("[red]Invalid input[/]");
                                        }
                    ))
                };

                if (!string.IsNullOrEmpty(dto.Email))
                    while (string.IsNullOrEmpty(dto.EmailPassword))
                        dto.EmailPassword = AnsiConsole.Prompt(
                                        new TextPrompt<string>("Email password:")
                                        .Secret('*'));

                await _userService.CreateAsync(dto, cancellationToken);
            }
            catch(Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
            }
        }

        public async Task DeleteUser(CancellationToken cancellationToken = default)
        {
            var userList = await _userService.GetList(cancellationToken);
            if (userList.Any())
            {
                var userToDelete = await AnsiConsole.PromptAsync(new SelectionPrompt<string>()
                                                .Title($"Choose user [red]to delete:[/]")
                                                .AddChoices(userList.Select(u => u.Login)));

                var password = await AnsiConsole.AskAsync<string>("Enter user [green]password[/]");
                await _userService.DeleteAsync(userToDelete, password);
            }
            else throw new NotFoundException("[red]Not found any users[/]");
        }

        public async Task UpdateUser(CancellationToken cancellationToken = default)
        {
            //var id = await _userService.
            //await _userService.UpdateAsync();
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
