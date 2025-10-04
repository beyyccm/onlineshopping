using Microsoft.AspNetCore.Identity;
using OnlineShopping.Business.Interfaces;
using OnlineShopping.DataAccess.Entities;
using OnlineShopping.DataAccess.Interfaces;
using System.Threading.Tasks;

namespace OnlineShopping.Business.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher<User> _passwordHasher;

        public UserService(IUnitOfWork unitOfWork, IPasswordHasher<User> passwordHasher)
        {
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
        }

        public async Task<bool> RegisterAsync(User user)
        {
            if (string.IsNullOrWhiteSpace(user.Password))
                throw new System.ArgumentException("Password is required for registration", nameof(user));

            user.PasswordHash = _passwordHasher.HashPassword(user, user.Password);
            user.Password = null;

            await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<User?> LoginAsync(string username, string password)
        {
            var user = await _unitOfWork.Users.GetByUsernameAsync(username);
            if (user == null) return null;

            var verification = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
            if (verification == PasswordVerificationResult.Success ||
                verification == PasswordVerificationResult.SuccessRehashNeeded)
                return user;

            return null;
        }
    }
}
