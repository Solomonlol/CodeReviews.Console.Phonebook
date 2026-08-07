using Backend.Interfaces;
using Backend.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Repositories
{
    internal class ContactRepository : IRepository<Contact>
    {

        private ApplicationContext _context;
        public ContactRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task Create(Contact item, CancellationToken cancellationToken = default)
        {
            await _context.Contacts.AddAsync(item);
        }

        public async Task Delete(Contact item, CancellationToken cancellationToken = default)
        {
            _context.Contacts.Remove(item);
        }
        public async Task Update(Contact item, CancellationToken cancellationToken = default)
        {
            _context.Entry(item).State = EntityState.Modified;
            return;
        }

        public async Task<Contact?> Get(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Contacts.FindAsync(id, cancellationToken);
        }

        public async Task<IEnumerable<Contact>> GetList(CancellationToken cancellationToken = default)
        {
            return await _context.Contacts.ToListAsync(cancellationToken);
        }
    }
}
