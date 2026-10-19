using Backend;
using Backend.Interfaces;
using Backend.Mapping;
using Backend.Models;
using Backend.Models.Dto;
using Backend.Repositories;
using Backend.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Text;

namespace PhoneBook.IntegrationTests
{
    public class ContactServiceTest : IClassFixture<IntegrationTestFixture>, IAsyncLifetime
    {
        private readonly IntegrationTestFixture _fixture;
        public ContactServiceTest(IntegrationTestFixture fixture)
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
            services.AddScoped<ContactService>();
            services.AddScoped<UserService>();

            return services.BuildServiceProvider();
        }

        public static IEnumerable<object[]> ValidContacts =>
            [
                [new ContactDto { Category = "Friends",
                FirstName = "Alex",
                MiddleName = "Greg",
                LastName = "Parker",
                Email = "Alex@gmail.com",
                PhoneNumber = "+375291234567" }],

                [new ContactDto { Category = "Family",
                FirstName = "Piter",
                MiddleName = "Spider",
                LastName = "Parker",
                Email = "Piter@gmail.com",
                PhoneNumber = "+375121234567" }],

                [new ContactDto { Category = "Friends",
                FirstName = "Solomon",
                MiddleName = "G",
                LastName = "Kane",
                Email = "Solomon@gmail.com",
                PhoneNumber = "+375127654321" }],
            ];


        [Theory]
        [MemberData(nameof(ValidContacts))]
        public async Task AddContactAsync_SaveInDatabase(ContactDto contact)
        {
            await using var provider = BuildProvider();

            await using var scope = provider.CreateAsyncScope();
            var db = scope.ServiceProvider
                .GetRequiredService<ApplicationContext>();

            await db.Database.MigrateAsync();

            var contactService = scope.ServiceProvider.GetRequiredService<ContactService>();
            var userService = scope.ServiceProvider.GetRequiredService<UserService>();
            var currentUser = scope.ServiceProvider.GetRequiredService<CurrentUserService>();

            var user = new CreateUserDto()
            {
                FirstName = "Alex",
                MiddleName = "Greg",
                LastName = "Parker",
                Login = "Root",
                Password = "SecretPassword123",
                Email = "Alex@gmail.com",
                EmailPassword = "password123",
                PhoneNumber = "+375291234567"
            };


            //Act
            await userService.CreateAsync(user);
            await userService.LogIn("Root", "SecretPassword123");
            
            await contactService.CreateAsync(contact);

            //Assert
            await using var checkDb = _fixture.CreateDbContext();
            var saved = await checkDb.Contacts.AsNoTracking().SingleOrDefaultAsync(c => c.FirstName == contact.FirstName);

            Assert.NotNull(saved);
            Assert.Equal(contact.FirstName, saved.FirstName);
            Assert.Equal(contact.Email, saved.Email);
            Assert.Equal(contact.PhoneNumber, saved.PhoneNumber);
        }
    }
}

