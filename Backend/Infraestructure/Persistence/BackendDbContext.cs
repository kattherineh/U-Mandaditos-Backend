using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Persistence
{
    public class BackendDbContext : DbContext
    {
        public BackendDbContext(DbContextOptions<BackendDbContext> options) : base(options)
        {
        }

        //Aquí se definen las tablas de la base de datos
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BackendDbContext).Assembly);
        }
    }
}
