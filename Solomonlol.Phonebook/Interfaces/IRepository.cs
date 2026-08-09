using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Backend.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T?> Get(int id, CancellationToken cancellationToken = default);
        Task<IEnumerable<T>> GetList(Expression<Func<T, bool>>? filter = null,
                                            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
                                            string properties = "",
                                            CancellationToken cancellationToken = default);
        Task Create(T item, CancellationToken cancellationToken = default);
        Task Update(T item, CancellationToken cancellationToken = default);
        Task Delete(int id, CancellationToken cancellationToken = default);
    }
}
