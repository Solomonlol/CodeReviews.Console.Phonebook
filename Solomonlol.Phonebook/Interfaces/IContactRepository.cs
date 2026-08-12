using Backend.Models;

namespace Backend.Interfaces
{
    public interface IContactRepository : IRepository<Contact>
    {
        Task<Contact?> GetByPhoneNumberAsync(string phoneNumber, CancellationToken cancellationToken = default);
    }
}
