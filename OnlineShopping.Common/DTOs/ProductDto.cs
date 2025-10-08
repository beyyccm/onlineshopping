using System.ComponentModel.DataAnnotations;

namespace OnlineShopping.Common.DTOs
{
    public class ProductDto
    {
        public int Id { get; set; }

        [Required, MinLength(2)]
        public string ProductName { get; set; } = null!;

        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue)]
        public int StockQuantity { get; set; }
    }
}
