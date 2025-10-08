using Microsoft.AspNetCore.Identity;

namespace OnlineShopping.DataAccess.Entities
{
    // Keep IdentityUser (string Id) to maintain compatibility with ASP.NET Identity
    public class User : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FullName => $"{FirstName} {LastName}";
        // Do NOT redefine Id, UserName, Email, PhoneNumber or PasswordHash here.
    }
}
