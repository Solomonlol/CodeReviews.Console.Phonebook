using Microsoft.EntityFrameworkCore;
using ShiftLogger.Backend.Entities;
using ShiftLogger.Backend.Interfaces;
using Solomonlol.ShiftLogger;


namespace ShiftLogger.Backend.Services
{
    public class ShiftService : IShiftService
    {
        private readonly ApplicationContext _context;
        public ShiftService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task Create(Shift shift, CancellationToken cancellationToken = default)
        {
            await _context.Shifts.AddAsync(shift, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task Delete(int id, CancellationToken cancellationToken = default)
        {
            var shift = await _context.Shifts.FindAsync(id, cancellationToken);
            _context.Shifts.Remove(shift);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<Shift>> GetAll(CancellationToken cancellationToken = default)
        {
            return await _context.Shifts.ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Shift>> GetAllByUserId(int userId, CancellationToken cancellationToken = default)
        {
            return await _context.Shifts.Where(s=>s.UserId==userId).ToListAsync(cancellationToken);
        }

        public async Task<Shift> GetById(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Shifts.FindAsync(id, cancellationToken);
        }

        public async Task Update(Shift shift, CancellationToken cancellationToken = default)
        {
            if (shift != null)
                _context.Shifts.Update(shift);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
