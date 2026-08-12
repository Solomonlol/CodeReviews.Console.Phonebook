using Backend.Models;

namespace Backend.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetByLogin(string login, CancellationToken cancellationToken=default);
    }
}
