using OnlineShopping.DataAccess.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineShopping.Business.Interfaces
{
    public interface ITokenService
    {
        Task<string> GenerateTokenAsync(User user, IList<string> roles);
    }
}
