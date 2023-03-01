using Microsoft.EntityFrameworkCore;
using Order.Domain;
using Order.Persistence.Database.Configuration;

namespace Order.Persistence.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base (options) 
        { 

        }
        public DbSet<ClientOrder> ClientOrders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("Order");
            ModelConfig(modelBuilder);
        }
        private void ModelConfig(ModelBuilder modelBuilder)
        {
            new ClientOrderConfiguration(modelBuilder.Entity<ClientOrder>());
        }
    }
}