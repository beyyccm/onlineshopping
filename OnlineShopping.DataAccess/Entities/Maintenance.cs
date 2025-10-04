using System;

namespace OnlineShopping.DataAccess.Entities
{
    public class Maintenance
    {
        public int Id { get; set; }
        public string Description { get; set; } = null!;
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
