using System.Threading.Tasks;
using OnlineShopping.DataAccess.Data;
using OnlineShopping.DataAccess.Interfaces;
using OnlineShopping.DataAccess.Repositories;

namespace OnlineShopping.DataAccess.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public IUserRepository Users { get; private set; }

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            Users = new UserRepository(_context);
        }

        public async Task CompleteAsync() => await _context.SaveChangesAsync();
    }
}
