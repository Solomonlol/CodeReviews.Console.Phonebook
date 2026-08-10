using Backend.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Interfaces
{
    public interface IContactRepository : IRepository<Contact>
    {
        Task<Contact?> GetByPhoneNumberAsync(string phoneNumber, CancellationToken cancellationToken = default);
    }
}
