using Backend.Interfaces;
using Backend.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Backend.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        
        public UserRepository(ApplicationContext context) : base(context) { }

        public async Task<User?> GetByLogin(string login, CancellationToken cancellationToken = default)
        {
            return await DbSet.FirstOrDefaultAsync(u=>u.Login == login, cancellationToken);
        }
    }
}
