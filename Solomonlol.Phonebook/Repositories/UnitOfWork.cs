using Backend.Interfaces;
using Backend.Models;

namespace Backend.Repositories
{
    internal class UnitOfWork : IDisposable, IUnitOfWork
    {
        private readonly ApplicationContext _context;
        public IRepository<User> _userRepository;
        public IRepository<Contact> _contactRepository;

        public UnitOfWork(ApplicationContext context, IRepository<User> users, IRepository<Contact> contacts)
        {
            _context = context;
            _userRepository = users;
            _contactRepository = contacts;
        }

        public IRepository<User> Users

        {
            get
            {
                if(_userRepository==null)
                {
                    _userRepository = new Repository<User>(_context);
                }
                return _userRepository;
            }
        }

        public IRepository<Contact> Contacts
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

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        private bool _disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if(!_disposed)
            {
                if(disposing)
                    _context?.Dispose();
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
