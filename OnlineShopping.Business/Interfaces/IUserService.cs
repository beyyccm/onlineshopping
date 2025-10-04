using OnlineShopping.DataAccess.Entities;
using System.Threading.Tasks;

namespace OnlineShopping.Business.Interfaces
{
    public interface IUserService
    {
        Task<bool> RegisterAsync(User user);
        Task<User?> LoginAsync(string username, string password);
    }
}
