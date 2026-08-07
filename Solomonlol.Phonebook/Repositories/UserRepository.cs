//using Backend.Interfaces;
//using Backend.Models;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace Backend.Repositories
//{
//    internal class UserRepository : IRepository<User>
//    {
//        private ApplicationContext _context;
//        public UserRepository(ApplicationContext context)
//        {
//            _context = context;
//        }
//        public async Task Create(User item, CancellationToken cancellationToken = default)
//        {
//            await _context.Users.AddAsync(item);
//        }
//        public async Task Update(User item, CancellationToken cancellationToken = default)
//        {
//            _context.Entry(item).State=EntityState.Modified;
//            await _context.SaveChangesAsync(cancellationToken);
//        }

//        public async Task Delete(int id, CancellationToken cancellationToken = default)
//        {
//            var item = await _context.Users.FindAsync(id, cancellationToken);
//            if(item!=null)
//                _context.Users.Remove(item);
//        }

//        public async Task<User?> Get(int id, CancellationToken cancellationToken = default)
//        {
//            return await _context.Users.FindAsync(id, cancellationToken);
//        }

//        public async Task<IEnumerable<User>> GetList(CancellationToken cancellationToken = default)
//        {
//            return await _context.Users.ToListAsync(cancellationToken);
//        }

//    }
//}
