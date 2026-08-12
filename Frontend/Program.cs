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
var db = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
var hasher = scope.ServiceProvider.GetRequiredService<IPasswordHasher<User>>();
//dbContext.Database.EnsureCreated();
await dbContext.Database.MigrateAsync();

var userService = scope.ServiceProvider.GetService<UserService>();
var contactService = scope.ServiceProvider.GetService<ContactService>();
var emailService  = scope.ServiceProvider.GetService<EmailService>();
var userMenu = scope.ServiceProvider.GetService<UserManagementMenu>();
var contactMenu = scope.ServiceProvider.GetService<ContactManagementMenu>();
var userInterface = scope.ServiceProvider.GetService<UserInterface>();
var mainMenu = scope.ServiceProvider.GetService<MainMenu>();

if (!db.Users.Any())
{
    var users = new List<User>
    {
        new() { Login = "ivanov",  FirstName = "Иван",   LastName = "Иванов",  MiddleName = "Петрович",      Email = "ivanov@mail.ru",   PhoneNumber = "+79001112233" },
        new() { Login = "petrova", FirstName = "Анна",   LastName = "Петрова", MiddleName = "Сергеевна",     Email = "petrova@gmail.com", PhoneNumber = "+79002223344" },
        new() { Login = "sidorov", FirstName = "Алексей",LastName = "Сидоров", MiddleName = null,            Email = "sidorov@yandex.ru", PhoneNumber = "+79003334455" },
        new() { Login = "smirnova",FirstName = "Мария",  LastName = "Смирнова",MiddleName = "Игоревна",      Email = "smirnova@mail.ru",  PhoneNumber = "+79004445566" },
        new() { Login = "kozlov",  FirstName = "Дмитрий",LastName = "Козлов",  MiddleName = "Александрович", Email = "kozlov@gmail.com",  PhoneNumber = "+79005556677" },
    };

    foreach (var user in users)
    {
        user.LoginPasswordHash = hasher.HashPassword(user, "Password123!");
        db.Users.Add(user);
    }

    await db.SaveChangesAsync();

    
    var contacts = new List<Contact>();
    int contactId = 1;

    string[] firstNames = { "Сергей", "Елена", "Павел", "Ольга", "Андрей" };
    string[] lastNames = { "Волков", "Морозова", "Новиков", "Фёдорова", "Соколов" };
    string[] categories = { "Друзья", "Работа", "Семья", "Друзья", "Работа" };

    foreach (var user in users)
    {
        for (int i = 0; i < 5; i++)
        {
            contacts.Add(new Contact
            {
                UserId = user.Id,
                FirstName = firstNames[i],
                LastName = lastNames[i],
                PhoneNumber = $"+790{user.Id}000{i + 1}{i + 1}{i + 1}{i + 1}",
                Email = $"{firstNames[i].ToLower()}{user.Id}@mail.ru",
                Category = categories[i]
            });
        }
    }

    db.Contacts.AddRange(contacts);
    await db.SaveChangesAsync();
}

await mainMenu.RunAsync();