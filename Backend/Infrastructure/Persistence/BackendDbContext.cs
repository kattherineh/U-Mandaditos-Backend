using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class BackendDbContext : DbContext
    {
        public BackendDbContext(DbContextOptions<BackendDbContext> options) : base(options)
        {
        }
        
        public DbSet<OrderStatus> OrderStatus { get; set; }
        public DbSet<Location> Locations { set; get; }
        public DbSet<Media> Media { set; get; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Location>()
                .Property(l => l.Latitude)
                .HasColumnType("DECIMAL(18,8)");

            modelBuilder.Entity<Location>()
                .Property(l => l.Longitude)
                .HasColumnType("DECIMAL(18,8)");
        }
    }
}
