using Frontend.Commands;
using Spectre.Console;

namespace Frontend
{
    internal class UserInterface
    {
        private readonly UserMenu _userMenu;
        private readonly ContactMenu _contactMenu;

        public UserInterface(UserMenu userMenu, ContactMenu contactMenu)
        {
            _userMenu = userMenu;
            _contactMenu = contactMenu;
        }

        public async Task Menu()
        {
            while (true)
            {
                await ViewSubMenu(_userMenu.GetSubMenu(), "Menu");
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
