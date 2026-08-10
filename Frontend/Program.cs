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
using Frontend.Commands;

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
        services.AddTransient<UserInterface>();
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

UserInterface user = new(userMenu, contactMenu);
await user.Menu();