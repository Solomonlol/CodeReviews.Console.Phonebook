namespace ShiftLogger.Backend.Interfaces
{
    public interface IDbService<T> where T : class
    {
        Task<T> GetById(int id, CancellationToken cancellationToken = default);
        Task<IEnumerable<T>> GetAll(CancellationToken cancellationToken = default);
        Task Delete(int id, CancellationToken cancellationToken = default);
        Task Update(T item, CancellationToken cancellationToken = default);
        Task Create(T item, CancellationToken cancellationToken = default);
    }
}
