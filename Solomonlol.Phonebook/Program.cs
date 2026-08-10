using Backend;
using Backend.Interfaces;
using Backend.Mapping;
using Backend.Models;
using Backend.Repositories;
using Backend.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
    }
    )
    .Build();

using (var scope = host.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationContext>();

    dbContext.Database.EnsureCreated();
    //await dbContext.Database.MigrateAsync();
}