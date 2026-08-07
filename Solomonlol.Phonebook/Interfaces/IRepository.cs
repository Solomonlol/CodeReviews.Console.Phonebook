using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Interfaces
{
    internal interface IRepository<T> where T : class
    {
        Task<T?> Get(int id, CancellationToken cancellationToken = default);
        Task<IEnumerable<T>> GetList(CancellationToken cancellationToken = default);
        Task Create(T item, CancellationToken cancellationToken = default);
        Task Update(T item, CancellationToken cancellationToken = default);
        Task Delete(T item, CancellationToken cancellationToken = default);
    }
}
