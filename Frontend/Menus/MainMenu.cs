using Backend.Exceptions;
using Backend.Services;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace Frontend.Menus
{
    internal class MainMenu : UserInterface
    {

        private readonly UserManagementMenu _userManagementMenu;
        private readonly LogInMenu _logInMenu;
        private readonly UserService _userService;

        public MainMenu(UserManagementMenu userManagementMenu, 
            LogInMenu logInMenu, 
            UserService service) : base("Main menu")
        {
            _userManagementMenu = userManagementMenu;
            _userService = service;
            _logInMenu = logInMenu;
            AddItem("Log In", () => LogIn());
            AddItem("User management", () => UserManagement());
            AddExitItem("Back");
        }

        public async Task LogIn(CancellationToken cancellationToken = default)
        {
            try
            {
                var loginList = await _userService.GetList();
                if (loginList.Any())
                {
                    var userToLogIn = await AnsiConsole.PromptAsync(new SelectionPrompt<string>()
                                                    .Title($"Choose user [green]to log in:[/]")
                                                    .AddChoices(loginList.Select(u => u.Login)));

                    var password = await AnsiConsole.AskAsync<string>("Enter user [green]password[/]");

                    var user = await _userService.LogIn(userToLogIn, password);
                    await _logInMenu.RunAsync(cancellationToken);
                }
                else throw new NotFoundException("Not found any users");
            }
            catch (NotFoundException ex)
            {
                AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
            }
            catch (ValidationException ex)
            {
                AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
            }
        }

        public async Task UserManagement(CancellationToken cancellationToken = default)
        {
            await _userManagementMenu.RunAsync(cancellationToken);
        }
    }
}
