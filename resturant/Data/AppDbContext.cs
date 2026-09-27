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

        public DbSet<Role> Roles { get; set; }

        public DbSet<Permission> Permissions { get; set; }

        public DbSet<PermissionRole> PermissionRoles { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<RoleUser> RoleUsers { get; set; }
        public DbSet<UserFile> UserFiles { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PermissionRole>()
                .HasKey(pr => new { pr.PermissionsId, pr.RolesId });

            modelBuilder.Entity<RoleUser>()
                .HasKey(ru => new { ru.UserId, ru.RoleId });
        }

    }
}
