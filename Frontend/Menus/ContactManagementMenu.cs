using Backend.Exceptions;
using Backend.Models.Dto;
using Backend.Services;
using Backend.Validation;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace Frontend.Menus
{
    internal class ContactManagementMenu : UserInterface
    {
        private readonly ContactService _contactService;

        public ContactManagementMenu(ContactService service) : base("Contact management")
        {
            _contactService = service;
            AddItem("Create contact", () => Create());
            AddItem("Update contact", () => Update());
            AddItem("Delete contact", () => Delete());
            AddExitItem("Back");
        }

        public async Task Create(CancellationToken cancellationToken = default)
        {
            try
            {
                var contact = InCreation.Creation<ContactDto>(propertyInputConfig);
                
                await _contactService.CreateAsync(contact, cancellationToken);
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
                var contactList = await _contactService.GetList(cancellationToken);
                if (contactList.Any())
                {
                    var contactToDelete = await AnsiConsole.PromptAsync(new MultiSelectionPrompt<ContactDto>()
                                                        .Title("Choose contact to [red]delete[/]:")
                                                        .UseConverter(c => $"{c.FirstName} | {c.LastName} | {c.MiddleName} | {c.PhoneNumber} | {c.Email} | {c.Category}")
                                                        .AddChoices(contactList));
                    foreach (var item in contactToDelete)
                    {
                        await _contactService.DeleteAsync(item, cancellationToken);
                    }
                }
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

        public async Task Update(CancellationToken cancellationToken = default)
        {
            try
            {
                var contactList = await _contactService.GetList(cancellationToken);
                if (contactList.Any())
                {
                    var contactToDelete = await AnsiConsole.PromptAsync(new SelectionPrompt<ContactDto>()
                                                        .Title("Choose contact to [red]update[/]:")
                                                        .UseConverter(c => $"{c.FirstName} | {c.LastName} | {c.MiddleName} | {c.PhoneNumber} | {c.Email} | {c.Category}")
                                                        .AddChoices(contactList));

                    await _contactService.UpdateAsync(contactToDelete, cancellationToken);
                }
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
                { nameof(ContactDto.Category), ()=> AnsiConsole.Ask<string>("Categoty:")},
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
