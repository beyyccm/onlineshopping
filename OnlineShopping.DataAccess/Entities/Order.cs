using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineShopping.DataAccess.Entities
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Required, Range(0.01, double.MaxValue)]
        public decimal TotalAmount { get; set; }

        // Use string to match IdentityUser.Id (string)
        [Required]
        public string CustomerId { get; set; } = null!;

        [ForeignKey("CustomerId")]
        public User Customer { get; set; } = null!;
    }
}
