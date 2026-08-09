using Frontend.Commands;
using Spectre.Console;

namespace Frontend
{
    internal class UserInterface
    {
        private readonly UserCommands _userCommands;

        public UserInterface(UserCommands userCommands)
        {
            _userCommands = userCommands;
        }

        public async Task Menu()
        {
            while (true)
            {
                await ViewSubMenu(_userCommands.GetSubMenu(), "Menu");
            }
        }

        private async Task ViewSubMenu(Dictionary<string, Func<Task>> menu, string title)
        {
            var choise = await AnsiConsole.PromptAsync(new SelectionPrompt<string>()
                                .Title($"[green]{title}[/]")
                                .AddChoices(menu.Keys));

            await menu[choise]();
        }
    }
}
