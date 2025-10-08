using System.ComponentModel.DataAnnotations;

namespace OnlineShopping.Common.DTOs
{
    public class RegisterDto
    {
        [Required, MinLength(3)]
        public string Username { get; set; } = null!;

        [Required, EmailAddress]
        public string Email { get; set; } = null!;

        [Required, MinLength(6)]
        public string Password { get; set; } = null!;

        public string? Role { get; set; } = "Customer";
    }

    public class LoginDto
    {
        [Required]
        public string Username { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;
    }
}
