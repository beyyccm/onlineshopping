using OnlineShopping.Common.DTOs;
using System.Threading.Tasks;

namespace OnlineShopping.Business.Interfaces
{
    public interface IUserService
    {
        Task<bool> RegisterAsync(RegisterDto registerDto);
        Task<string?> LoginAsync(LoginDto loginDto);
    }
}
