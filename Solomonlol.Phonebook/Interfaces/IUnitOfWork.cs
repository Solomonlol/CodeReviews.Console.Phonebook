using Backend.Models;

namespace Backend.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<User> Users {  get; }
        IRepository<Contact> Contacts { get; }

        Task<int> SaveAsync(CancellationToken cancellationToken);
    }
}
