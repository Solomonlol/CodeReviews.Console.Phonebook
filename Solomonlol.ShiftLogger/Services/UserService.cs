using Microsoft.EntityFrameworkCore;
using ShiftLogger.Backend.Entities;
using ShiftLogger.Backend.Interfaces;
using Solomonlol.ShiftLogger;

namespace ShiftLogger.Backend.Services
{
    public class UserService : IDbService<User>
    {
        private readonly ApplicationContext _context;
        public UserService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task Create(User user, CancellationToken cancellationToken)
        {
            await _context.Users.AddAsync(user);
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FindAsync(id, cancellationToken);
            if(user!=null)
                _context.Users.Remove(user);
        }

        public async Task<IEnumerable<User>> GetAll(CancellationToken cancellationToken)
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetById(int id, CancellationToken cancellationToken)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task Update(int id, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FindAsync(id);
            if(user!=null)
                _context.Users.Update(user);
        }
    }
}
