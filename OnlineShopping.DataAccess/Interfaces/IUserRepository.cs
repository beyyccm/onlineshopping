using OnlineShopping.DataAccess.Entities;
using System.Threading.Tasks;

namespace OnlineShopping.DataAccess.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User?> GetByUsernameAsync(string username);
    }
}
