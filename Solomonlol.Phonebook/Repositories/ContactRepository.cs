using Backend.Interfaces;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories
{
    public class ContactRepository : Repository<Contact>, IContactRepository
    {
        public ContactRepository(ApplicationContext context) : base(context) { }

        public async Task<Contact?> GetByPhoneNumberAsync(string phoneNumber, CancellationToken cancellationToken = default)
        {
            return await DbSet.FirstOrDefaultAsync(c=>c.PhoneNumber == phoneNumber);
        }
    }
}
