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
        public DbSet<Career> Careers { set; get; }
        public DbSet<User> Users { set; get; }
        
        public DbSet<SessionLog> SessionLogs { set; get; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<User>()
                .HasOne(u=> u.ProfilePic) //Tiene una Foto de perfil
                .WithOne()
                .HasForeignKey<User>(u=> u.ProfilePicId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<User>()
                .HasOne(u => u.LastLocation) //Tiene una location
                .WithOne()
                .HasForeignKey<User>(u => u.LastLocationId)
                .OnDelete(DeleteBehavior.SetNull);
            
            modelBuilder.Entity<User>()
                .HasOne(u => u.Career) //Tiene una carrera
                .WithOne()
                .HasForeignKey<User>(u => u.CareerId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<SessionLog>()
                .HasOne(s => s.User)
                .WithOne()
                .HasForeignKey<SessionLog>(s => s.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<Location>()
                .Property(l => l.Latitude)
                .HasColumnType("DECIMAL(18,8)");

            modelBuilder.Entity<Location>()
                .Property(l => l.Longitude)
                .HasColumnType("DECIMAL(18,8)");
        }
    }
}
