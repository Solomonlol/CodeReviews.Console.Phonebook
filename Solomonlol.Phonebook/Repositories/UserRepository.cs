using Backend.Interfaces;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

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
