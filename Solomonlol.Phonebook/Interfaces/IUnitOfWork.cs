using Backend.Models;

namespace Backend.Interfaces
{
    internal interface IUnitOfWork
    {
        IRepository<User> Users {  get; }
        IRepository<Contact> Contacts { get; }

        Task<int> SaveAsync();
    }
}
