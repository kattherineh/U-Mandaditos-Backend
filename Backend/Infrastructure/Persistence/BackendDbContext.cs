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
        public DbSet<Offer> Offers { set; get; }
        public DbSet<Mandadito> Mandaditos { set; get; }
        public DbSet<Rating> Ratings { set; get; }
        public DbSet<OrderStatusHistory> OrderStatusHistories { set; get; }
        public DbSet<Message> Messages { set; get; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasOne(u => u.ProfilePic) //Tiene una Foto de perfil
                .WithOne()
                .HasForeignKey<User>(u => u.ProfilePicId)
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

            modelBuilder.Entity<Offer>(entity =>
            {
                entity.HasKey(o => o.Id);

                entity.Property(o => o.CounterOfferAmount)
                    .IsRequired()
                    .HasColumnType("DECIMAL(18,2)");

                entity.HasOne(o => o.UserCreator)
                    .WithMany()
                    .HasForeignKey(o => o.IdUserCreator)
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                entity.HasOne(o => o.Post)
                    .WithMany()
                    .HasForeignKey(o => o.IdPost)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.Property(o => o.CreatedAt)
                    .HasDefaultValueSql("GETDATE()");

                entity.Property(o => o.IsCounterOffer)
                    .IsRequired();

                entity.Property(o => o.Accepted)
                    .HasDefaultValue(false);

            });

            modelBuilder.Entity<Mandadito>(entity =>
            {
                entity.HasKey(m => m.Id);

                entity.Property(m => m.SecurityCode)
                    .IsRequired()
                    .HasMaxLength(6);

                entity.Property(m => m.AcceptedRate)
                    .IsRequired()
                    .HasColumnType("DECIMAL(18,2)");

                entity.HasOne(m => m.Post)
                    .WithMany()
                    .IsRequired()
                    .HasForeignKey(m => m.IdPost)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(m => m.Offer)
                    .WithMany()
                    .HasForeignKey(m => m.IdOffer)
                    .OnDelete(DeleteBehavior.Cascade);

            });

            modelBuilder.Entity<Rating>(entity =>
            {
                entity.HasKey(r => r.Id);

                entity.HasOne(r => r.Mandadito)
                    .WithMany()
                    .HasForeignKey(r => r.IdMandadito)
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                entity.HasOne(r => r.RatedUser)  // Usuario calificado
                    .WithMany()
                    .HasForeignKey(r => r.IdRatedUser)
                    .OnDelete(DeleteBehavior.NoAction)
                    .IsRequired();

                entity.HasOne(r => r.RaterUser)  // Usuario que califica
                    .WithMany()
                    .HasForeignKey(r => r.IdRater)
                    .OnDelete(DeleteBehavior.NoAction)
                    .IsRequired();

                entity.HasOne(r => r.RatedRole)
                    .WithMany()
                    .HasForeignKey(r => r.IdRatedRole)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);  

                entity.Property(r => r.RatingNum)
                    .IsRequired();

                entity.Property(r => r.CreatedAt)
                    .HasDefaultValueSql("GETDATE()");
            });

            modelBuilder.Entity<OrderStatusHistory>(entity =>
            {
                entity.HasKey(osh => osh.Id);

                entity.HasOne(osh => osh.OrderStatus)
                    .WithMany()
                    .HasForeignKey(osh => osh.IdStatus)
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                entity.HasOne(osh => osh.Mandadito)
                    .WithMany()
                    .HasForeignKey(osh => osh.IdMandadito)
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                entity.Property(osh => osh.ChangeAt)
                    .HasDefaultValueSql("GETDATE()")
                    .IsRequired();

                entity.Property(osh => osh.Active)
                    .HasDefaultValue(true)
                    .IsRequired();
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.HasKey(m => m.Id);

                entity.HasOne(m => m.User)
                    .WithMany()
                    .HasForeignKey(m => m.IdUser)
                    .OnDelete(DeleteBehavior.NoAction)
                    .IsRequired();

                entity.HasOne(m => m.Mandadito)
                    .WithMany()
                    .HasForeignKey(m => m.IdMandadito)
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                entity.Property(m => m.Text)
                    .IsRequired();

                entity.Property(m => m.CreatedAt)
                    .HasDefaultValueSql("GETDATE()")
                    .IsRequired();
            });

        }
    }
}