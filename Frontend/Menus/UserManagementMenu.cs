using Backend.Models.Dto;
using Backend.Services;
using Spectre.Console;
using Backend.Validation;
using Backend.Exceptions;
using System.Reflection;

namespace Frontend.Menus
{
    internal class UserManagementMenu : UserInterface
    {
        private readonly UserService _userService;
        
        public UserManagementMenu(UserService service) : base("User management")
        {
            _userService = service;
            AddItem("Create user", ()=> Create());
            AddItem("Update user", () => Update());
            AddItem("Delete user", () => Delete());
            AddExitItem("Back");
        }

        public async Task Create(CancellationToken cancellationToken = default)
        {
            try
            {
                var dto = InCreation.Creation<CreateUserDto>(propertyInputConfig, typeof(CreateUserDto).GetProperties().ToList());
                if (!string.IsNullOrEmpty(dto.Email))
                    while (string.IsNullOrEmpty(dto.EmailPassword))
                        dto.EmailPassword = AnsiConsole.Prompt(
                                        new TextPrompt<string>("Email password:")
                                        .Secret('*'));

                await _userService.CreateAsync(dto, cancellationToken);
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

        public async Task Delete(CancellationToken cancellationToken = default)
        {
            try
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
            catch(NotFoundException ex)
            {
                AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
            }
            catch(ValidationException ex)
            {
                AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
            }
        }

        public async Task Update(CancellationToken cancellationToken = default)
        {
            try
            {
                var list = await _userService.GetList(cancellationToken);
                if (list.Any())
                {
                    var choise = await AnsiConsole.PromptAsync(new SelectionPrompt<string>()
                                                    .Title($"Choose user to update")
                                                    .AddChoices(list.Select(u => u.Login)));

                    var currentUser = list.First(u => u.Login == choise);
                    

                    var choisesToUpdate = await AnsiConsole.PromptAsync(new MultiSelectionPrompt<PropertyInfo>()
                                                            .Title("Choose what to update:")
                                                            .UseConverter(p=>p.Name)
                                                            .AddChoices(currentUser.GetType().GetProperties()));

                    var updatedUser = InCreation.Creation<UserDto>(propertyInputConfig, choisesToUpdate);

                    await _userService.UpdateAsync(currentUser, updatedUser);
                }
                else throw new NotFoundException("Not found any records to update");
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

        private readonly Dictionary<string, Func<object>> propertyInputConfig = new Dictionary<string, Func<object>>
            {
                { nameof(CreateUserDto.Login), ()=> AnsiConsole.Ask<string>("Login:")},
                { nameof(CreateUserDto.Password), ()=> AnsiConsole.Prompt(
                                                        new TextPrompt<string>("Password:")
                                                        .Secret('*'))},
                { nameof(CreateUserDto.PhoneNumber), ()=> AnsiConsole.Prompt(
                                                        new TextPrompt<string>("Phone number in format: \n'+ xxx (xxx) xxx-xx-xx'\n+, -, spaces, brackets is not nessecery")
                                                        .Validate(input =>
                                                        {
                                                            return Validation.IsPhoneNumber(input)
                                                            ? ValidationResult.Success()
                                                            : ValidationResult.Error("[red]Invalid input[/]");
                                                        }))},
                { nameof(CreateUserDto.FirstName), ()=> AnsiConsole.Ask<string>("First name:")},
                { nameof(CreateUserDto.LastName), ()=> AnsiConsole.Prompt(
                                                        new TextPrompt<string>("Last name (optional):")
                                                        .AllowEmpty())},
                { nameof(CreateUserDto.MiddleName), ()=> AnsiConsole.Prompt(
                                                        new TextPrompt<string>("Middle name (optional):")
                                                        .AllowEmpty())},
                { nameof(CreateUserDto.Email), ()=> AnsiConsole.Prompt(
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
                                        )) }
            };



    }
}
