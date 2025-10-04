using System.ComponentModel.DataAnnotations;

namespace OnlineShopping.Common.DTOs
{
    public class RegisterDto
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        [Phone]
        public string PhoneNumber { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;

        [Required]
        public string Role { get; set; } = "Customer";
    }
}
