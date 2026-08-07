using Backend.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Repositories
{
    internal class UnitOfWork : IDisposable
    {
        private ApplicationContext _context = new ApplicationContext();
        private Repository<User> _userRepisitory;
        private Repository<Contact> _contactRepository;

        public Repository<User> UserRepository
        {
            get
            {
                if(_userRepisitory==null)
                {
                    _userRepisitory = new Repository<User>(_context);
                }
                return _userRepisitory;
            }
        }

        public Repository<Contact> ContactRepository
        {
            get
            {
                if (_contactRepository == null)
                {
                    _contactRepository = new Repository<Contact>(_context);
                }
                return _contactRepository;
            }
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        private bool _disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if(!_disposed)
            {
                _context.Dispose();
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
