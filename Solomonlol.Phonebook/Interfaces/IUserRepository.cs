using Backend.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetByLogin(string login, CancellationToken cancellationToken=default);
    }
}
