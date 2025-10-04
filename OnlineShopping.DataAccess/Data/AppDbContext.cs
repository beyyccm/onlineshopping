using Microsoft.EntityFrameworkCore;
using OnlineShopping.DataAccess.Entities;

namespace OnlineShopping.DataAccess.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}
