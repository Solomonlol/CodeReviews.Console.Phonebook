using Backend;
using Backend.Interfaces;
using Backend.Repositories;
using Backend.Services;
using Backend.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Backend.Models;
using Microsoft.AspNetCore.Identity;
using Frontend;
using Frontend.Menus;

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        string? connectionString = context.Configuration.GetConnectionString("PostgresConnection");
        services.AddDbContext<ApplicationContext>(options =>
            options.UseNpgsql(connectionString));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IContactRepository, ContactRepository>();
        services.AddAutoMapper(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });
        services.AddScoped<UserService>();
        services.AddScoped<ContactService>();
        services.AddScoped<EmailService>();
        services.AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>();

        services.AddTransient<UserManagementMenu>();
        services.AddTransient<ContactManagementMenu>();
        services.AddTransient<MainMenu>();
        services.AddTransient<LogInMenu>();
        services.AddTransient<EmailMenu>();
        services.AddScoped<IMenu, UserManagementMenu>();
        services.AddScoped<IMenu, ContactManagementMenu>();
        services.AddScoped<IMenu, UserInterface>();
        services.AddSingleton<CurrentUserService>();
        services.AddDataProtection();
        services.AddSingleton<EmailPasswordProtection>();

        services.Configure<SmtpSettings>(context.Configuration.GetSection("SmtpSettings"));
        services.AddScoped<IMessage, EmailService>();

    }
    )
    .Build();

using var scope = host.Services.CreateScope();

var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
dbContext.Database.EnsureCreated();

var userService = scope.ServiceProvider.GetService<UserService>();
var contactService = scope.ServiceProvider.GetService<ContactService>();
var emailService  = scope.ServiceProvider.GetService<EmailService>();
var userMenu = scope.ServiceProvider.GetService<UserManagementMenu>();
var contactMenu = scope.ServiceProvider.GetService<ContactManagementMenu>();
var userInterface = scope.ServiceProvider.GetService<UserInterface>();
var mainMenu = scope.ServiceProvider.GetService<MainMenu>();

//var mainMenu = new UserInterface("Main menu");
//mainMenu.AddSubMenu("User menu", main);
////mainMenu.AddSubMenu("Contact menu", contactMenu);
//mainMenu.AddExitItem("Exit app");

await mainMenu.RunAsync();