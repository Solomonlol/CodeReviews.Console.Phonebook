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

        services.AddTransient<UserMenu>();
        //services.AddTransient<UserInterface>();
        services.AddScoped<IMenu, UserMenu>();
        services.AddScoped<IMenu, ContactMenu>();
        services.AddScoped<IMenu, UserInterface>();

    }
    )
    .Build();

using var scope = host.Services.CreateScope();

var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
dbContext.Database.EnsureCreated();

var userService = scope.ServiceProvider.GetService<UserService>();
var contactService = scope.ServiceProvider.GetService<ContactService>();
var emailService  = scope.ServiceProvider.GetService<EmailService>();
var userMenu = scope.ServiceProvider.GetService<UserMenu>();
var contactMenu = scope.ServiceProvider.GetService<ContactMenu>();
var mainMenu = scope.ServiceProvider.GetService<UserInterface>();

var userInterface = new UserInterface("Main menu");
userInterface.AddSubMenu("User menu", userMenu);
userInterface.AddSubMenu("Contact menu", contactMenu);
userInterface.AddExitItem("Exit");

await userInterface.RunAsync();