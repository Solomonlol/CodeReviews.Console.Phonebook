using Backend.Services;
using Frontend.Entity;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace Frontend.Menus
{
    internal class UserInterface : IMenu
    {

        private readonly string _title;
        private readonly List<MenuItem> _items = new();
        private bool _exit = false;

        public UserInterface(string title) => _title = title;

        public void AddItem(string name, Func<Task> action) 
            => _items.Add(new MenuItem(name, action));

        public void AddSubMenu(string name, IMenu subMenu) 
            => _items.Add(new MenuItem(name,subMenu));

        public void AddExitItem(string name = "Back")
            => AddItem(name, () => { _exit = true; return Task.CompletedTask; });

        public async  Task RunAsync(CancellationToken cancellationToken = default)
        {
            _exit = false;
            while(!_exit)
            {
                var choises = _items.Select(i => i.Name).ToList();
                var choise = await AnsiConsole.PromptAsync(
                    new SelectionPrompt<string>()
                    .Title($"[green]{_title}[/]")
                    .AddChoices(choises));

                var selected = _items.First(i => i.Name == choise);
                if (selected.Action != null)
                    await selected.Action();
                else if(selected.SubMenu!=null)
                    await selected.SubMenu.RunAsync(cancellationToken);
            }
        }
    }
}
