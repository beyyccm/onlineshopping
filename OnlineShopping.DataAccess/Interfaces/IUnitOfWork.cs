using System.Threading.Tasks;

namespace OnlineShopping.DataAccess.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
        Task CompleteAsync();
    }
}
