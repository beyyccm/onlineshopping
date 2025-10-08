using System.ComponentModel.DataAnnotations;

namespace OnlineShopping.DataAccess.Entities
{
    public class Maintenance
    {
        [Key]
        public int Id { get; set; } // <-- Primary Key alan� eklendi

        public bool IsMaintenance { get; set; }
        public string Message { get; set; }
    }
}
