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
        
        public DbSet<UserRole> UserRoles { set; get; }
        
        public DbSet<Post> Posts { get; set; }
        
        public DbSet<UserLocationHistory> UserLocationHistories { get; set; }

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
            
            modelBuilder.Entity<UserRole>()
                .Property(ur => ur.Name)
                .IsRequired()  // El nombre del rol no puede ser nulo
                .HasMaxLength(50);  // Longitud máxima de 50 caracteres
            
            modelBuilder.Entity<Post>(entity =>
            {
                entity.HasKey(p => p.Id);

                entity.Property(p => p.Description)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(p => p.SugestedValue)
                    .HasColumnType("DECIMAL(18,2)");

                entity.Property(p => p.CreatedAt)
                    .HasDefaultValueSql("GETDATE()");

                entity.Property(p => p.Completed)
                    .HasDefaultValue(false);

                entity.HasOne(p => p.PosterUser)
                    .WithMany()
                    .HasForeignKey(p => p.IdPosterUser)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(p => p.PickUpLocation)
                    .WithMany()
                    .HasForeignKey(p => p.IdPickUpLocation)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(p => p.DeliveryLocation)
                    .WithMany()
                    .HasForeignKey(p => p.IdDeliveryLocation)
                    .OnDelete(DeleteBehavior.NoAction);
            });
            
            modelBuilder.Entity<UserLocationHistory>(entity =>
            {
                entity.HasKey(ulh => new { ulh.IdUser, ulh.IdLocation });

                entity.Property(ulh => ulh.CreatedAt)
                    .IsRequired();

                entity.Property(ulh => ulh.Active)
                    .IsRequired();

                entity.HasOne(ulh => ulh.User)
                    .WithMany()
                    .HasForeignKey(ulh => ulh.IdUser)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(ulh => ulh.Location)
                    .WithMany()
                    .HasForeignKey(ulh => ulh.IdLocation)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}