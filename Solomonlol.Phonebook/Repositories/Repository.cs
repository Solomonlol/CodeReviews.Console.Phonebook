using Backend.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Backend.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        internal ApplicationContext Context;

        internal DbSet<T> DbSet;

        public Repository(ApplicationContext context)
        {
            Context = context;
            DbSet = context.Set<T>();
        }

        public async Task Create(T item, CancellationToken cancellationToken = default)
        {
            await DbSet.AddAsync(item, cancellationToken);
        }

        public async Task Delete(int id, CancellationToken cancellationToken = default)
        {
            T? item = await DbSet.FindAsync(id, cancellationToken);
            if(item!=null)
                DbSet.Remove(item);
        }
        public async Task Delete(T item, CancellationToken cancellationToken = default)
        {
            if(Context.Entry(item).State==EntityState.Detached)
            {
                DbSet.Attach(item);
            }
            DbSet.Remove(item);
        }

        public Task Update(T item, CancellationToken cancellationToken = default)
        {
            DbSet.Attach(item);
            Context.Entry(item).State = EntityState.Modified;
            return Task.CompletedTask;
        }

        public async Task<T?> Get(int id, CancellationToken cancellationToken = default)
        {
            return await DbSet.FindAsync(id, cancellationToken);
        }

        public async Task<IEnumerable<T>> GetList(Expression<Func<T, bool>>? filter = null,
                                            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
                                            string properties = "",
                                            CancellationToken cancellationToken = default)
        {
            IQueryable<T> query = DbSet;

            if(filter!=null)
            {
                query = query.Where(filter);
            }

            foreach(var property in properties.Split(
                                    new char[] {','}, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(property);
            }

            if(orderBy!=null)
            {
                return await orderBy(query).ToListAsync();
            }
            else
            {
                return await query.ToListAsync();
            }
        }

    }
}
