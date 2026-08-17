using Microsoft.EntityFrameworkCore;
using ShiftLogger.Backend.Entities;
using ShiftLogger.Backend.Interfaces;
using Solomonlol.ShiftLogger;

namespace ShiftLogger.Backend.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationContext _context;
        public UserService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task Create(User user, CancellationToken cancellationToken = default)
        {
            await _context.Users.AddAsync(user, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task Delete(int id, CancellationToken cancellationToken = default)
        {
            var user = await _context.Users.FindAsync(id, cancellationToken);
            if(user!=null)
                _context.Users.Remove(user);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<User>> GetAll(CancellationToken cancellationToken = default)
        {
            return await _context.Users.ToListAsync(cancellationToken);
        }

        public async Task<User> GetById(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Users.FindAsync(id, cancellationToken);
        }

        public async Task Update(User user, CancellationToken cancellationToken = default)
        {
            if(user!=null)
                _context.Users.Update(user);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
