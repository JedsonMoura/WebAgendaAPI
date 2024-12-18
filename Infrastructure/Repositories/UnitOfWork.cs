using Domain.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private IContactRepository _contactRepository;
        private IUserRepository _userRepository;
        private readonly AppDbContext _appDbContext;


        public UnitOfWork(AppDbContext context)
        {
            _appDbContext = context;
        }

        public IContactRepository ContactRepository
        { 
            get {
                return _contactRepository = _contactRepository ??
                    new ContactRepository(_appDbContext);

            } 
        }

        public IUserRepository UserRepository
        {
            get
            {
                return _userRepository = _userRepository ??
                    new UserRepository(_appDbContext);

            }
        }

        public async Task CommitAsync()
        {
            await _appDbContext.SaveChangesAsync();
        }

        public void Dispose()
        { 
            _appDbContext.Dispose();
        }

    }
}
