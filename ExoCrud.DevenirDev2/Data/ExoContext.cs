using ExoCrud.DevenirDev2.Models.Entites;
using Microsoft.EntityFrameworkCore;

namespace ExoCrud.DevenirDev2.Data
{
    public class ExoContext : DbContext
    {
        public ExoContext(DbContextOptions<ExoContext> options) :base(options)
        {
            
        }


        public DbSet<Car> Cars { get; set; }

        public DbSet<User> Users { get; set; }  


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ExoContext).Assembly);
        }
    }
}
