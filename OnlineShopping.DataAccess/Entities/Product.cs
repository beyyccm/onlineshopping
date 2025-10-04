using System.ComponentModel.DataAnnotations;

namespace OnlineShopping.DataAccess.Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string ProductName { get; set; } = null!;

        [Required, Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }

        [Required, Range(0, int.MaxValue)]
        public int StockQuantity { get; set; }
    }
}
