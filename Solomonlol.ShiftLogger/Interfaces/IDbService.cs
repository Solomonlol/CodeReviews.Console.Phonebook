namespace ShiftLogger.Backend.Interfaces
{
    public interface IDbService<T> where T : class
    {
        Task<T> GetById(int id, CancellationToken cancellationToken);
        Task<IEnumerable<T>> GetAll(CancellationToken cancellationToken);
        Task Delete(int id, CancellationToken cancellationToken);
        Task Update(int id, CancellationToken cancellationToken);
        Task Create(T item, CancellationToken cancellationToken);
    }
}
