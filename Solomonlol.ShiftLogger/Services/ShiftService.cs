using Microsoft.EntityFrameworkCore;
using ShiftLogger.Backend.Entities;
using ShiftLogger.Backend.Interfaces;
using Solomonlol.ShiftLogger;


namespace ShiftLogger.Backend.Services
{
    public class ShiftService : IDbService<Shift>
    {
        private readonly ApplicationContext _context;
        public ShiftService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task Create(Shift shift, CancellationToken cancellationToken)
        {
            await _context.Shifts.AddAsync(shift);
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            var shift = await _context.Shifts.FindAsync(id);
            _context.Shifts.Remove(shift);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Shift>> GetAll(CancellationToken cancellationToken)
        {
            return await _context.Shifts.ToListAsync();
        }

        public async Task<Shift> GetById(int id, CancellationToken cancellationToken)
        {
            return await _context.Shifts.FindAsync(id);
        }

        public async Task Update(int id, CancellationToken cancellationToken)
        {
            var shift = await _context.Shifts.FindAsync(id);
            if (shift != null)
                _context.Shifts.Update(shift);
            await _context.SaveChangesAsync();
        }
    }
}
