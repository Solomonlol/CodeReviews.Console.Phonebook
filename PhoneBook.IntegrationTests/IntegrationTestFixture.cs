using Microsoft.EntityFrameworkCore;
using Testcontainers.PostgreSql;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using Backend;
using Backend.Repositories;
using Backend.Interfaces;

namespace PhoneBook.IntegrationTests
{
    public sealed class IntegrationTestFixture : IAsyncLifetime
    {
        private readonly PostgreSqlContainer _container =
            new PostgreSqlBuilder()
            .WithImage("postgres:17-alpine")
            .WithDatabase("integration_test")
            .WithUsername("postgres")
            .WithPassword("postgres")
            .Build();
        public string ConnectionString => _container.GetConnectionString();

        public async Task DisposeAsync()
        {
            await _container.DisposeAsync();
        }

        public async Task InitializeAsync()
        {
            await _container.StartAsync();

            await using var db = CreateDbContext();

            await db.Database.MigrateAsync();
        }

        public ApplicationContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseNpgsql(ConnectionString)
                .Options;
            return new ApplicationContext(options);
        }

        public async Task ResetAsync()
        {
            await using var db = CreateDbContext();
            await db.Users.ExecuteDeleteAsync();
        }
    }
}
