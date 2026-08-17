using ShiftLogger.Backend.Entities;

namespace ShiftLogger.Backend.Interfaces
{
    public interface IShiftService
    {
        Task<Shift> GetById(int id, CancellationToken cancellationToken = default);
        Task<IEnumerable<Shift>> GetAll(CancellationToken cancellationToken = default);
        Task<IEnumerable<Shift>> GetAllByUserId(int userId, CancellationToken cancellationToken = default);
        Task Delete(int id, CancellationToken cancellationToken = default);
        Task Update(Shift item, CancellationToken cancellationToken = default);
        Task Create(Shift item, CancellationToken cancellationToken = default);
    }
}
