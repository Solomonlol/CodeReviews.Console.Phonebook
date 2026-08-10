using Backend.Models;

namespace Backend.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository Users {  get; }
        IContactRepository Contacts { get; }

        Task<int> SaveAsync(CancellationToken cancellationToken);
    }
}
