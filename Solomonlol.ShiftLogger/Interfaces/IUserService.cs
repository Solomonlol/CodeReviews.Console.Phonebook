using ShiftLogger.Backend.Entities;

namespace ShiftLogger.Backend.Interfaces
{
    public interface IUserService
    {
        Task<User> GetById(int id, CancellationToken cancellationToken = default);
        Task<IEnumerable<User>> GetAll(CancellationToken cancellationToken = default);
        Task Delete(int id, CancellationToken cancellationToken = default);
        Task Update(User item, CancellationToken cancellationToken = default);
        Task Create(User item, CancellationToken cancellationToken = default);
    }
}
