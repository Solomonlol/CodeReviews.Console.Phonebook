using AutoMapper;
using Backend;
using Backend.Interfaces;
using Backend.Mapping;
using Backend.Models;
using Backend.Models.Dto;
using Backend.Repositories;
using Backend.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.ConstrainedExecution;
using Xunit;

namespace PhoneBook.IntegrationTests
{
    public sealed class UserServiceTest : IClassFixture<IntegrationTestFixture>, IAsyncLifetime
    {
        private readonly IntegrationTestFixture _fixture;
        public UserServiceTest(IntegrationTestFixture fixture)
        {
            _fixture = fixture;
        }

        public Task InitializeAsync() => _fixture.ResetAsync();
        public Task DisposeAsync() => Task.CompletedTask;

        private ServiceProvider BuildProvider()
        {
            var services = new ServiceCollection();
            
            services.AddDbContext<ApplicationContext>(options =>
            options.UseNpgsql(_fixture.ConnectionString));

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IContactRepository, ContactRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddLogging();

            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            services.AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>();
            services.AddDataProtection();
            services.AddScoped<CurrentUserService>();
            services.AddScoped<EmailPasswordProtection>();
            services.AddScoped<UserService>();

            return services.BuildServiceProvider();
        }

        [Fact]
        public async Task AddUserAsync_SavesToDatabase()
        {
            //Arrange
            var user = new CreateUserDto()
            {
                FirstName = "Alex",
                MiddleName = "Greg",
                LastName = "Parker",
                Login = "Root",
                Password="SecretPassword123",
                Email = "Alex@gmail.com",
                EmailPassword ="password123",
                PhoneNumber = "+375291234567"
            };

            await using var provider = BuildProvider();

            await using var scope = provider.CreateAsyncScope();
            var db = scope.ServiceProvider
                .GetRequiredService<ApplicationContext>();

            await db.Database.MigrateAsync();

            var userService = scope.ServiceProvider.GetRequiredService<UserService>();

            //Act

            await userService.CreateAsync(user);

            //Assert
            await using var checkDb = _fixture.CreateDbContext();
            var saved = await checkDb.Users.AsNoTracking().SingleOrDefaultAsync(u => u.Login == "Root");

            Assert.NotNull(saved);
            Assert.Equal("Alex", saved.FirstName);
            Assert.Equal("Alex@gmail.com", saved.Email);
            Assert.Equal("+375291234567", saved.PhoneNumber);

            Assert.NotEqual("SecretPassword123", saved.LoginPasswordHash);
            Assert.NotEqual("password123", saved.EmailPasswordProtected);
        }
    }

}
