using Microsoft.EntityFrameworkCore;
using resturant.Models;

namespace resturant.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) :base(options)
        {
            
        }

        public DbSet<MenuItem> Menu { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
