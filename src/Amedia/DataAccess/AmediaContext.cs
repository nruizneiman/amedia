using Amedia.Domain;
using Amedia.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace Amedia.DataAccess
{
    public class AmediaContext : DbContext
    {
        public AmediaContext(DbContextOptions<AmediaContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRole>().HasData(new UserRole { Id = 1, Name = "Administrator", RelativeStartupPagePath = "/Users/Management" });
            modelBuilder.Entity<UserRole>().HasData(new UserRole { Id = 2, Name = "Guest", RelativeStartupPagePath = "/Users/Index" });

            modelBuilder.Entity<User>().HasData(new User { Id = 1, UserName = "admin", Password = SecurityHelper.HashPassword("admin"), RoleId = 1 });

            base.OnModelCreating(modelBuilder);
        }
    }
}
