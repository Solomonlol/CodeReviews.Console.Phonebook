using Backend.Interfaces;
using Backend.Models;

namespace Backend.Repositories
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private readonly ApplicationContext _context;
        public IUserRepository _userRepository;
        public IContactRepository _contactRepository;

        public UnitOfWork(ApplicationContext context, IUserRepository users, IContactRepository contacts)
        {
            _context = context;
            _userRepository = users;
            _contactRepository = contacts;
        }

        public IUserRepository Users
        {
            get
            {
                if(_userRepository==null)
                {
                    _userRepository = new UserRepository(_context);
                }
                return _userRepository;
            }
        }

        public IContactRepository Contacts
        {
            get
            {
                if (_contactRepository == null)
                {
                    _contactRepository = new ContactRepository(_context);
                }
                return _contactRepository;
            }
        }

        public async Task<int> SaveAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
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
