using Microsoft.EntityFrameworkCore;
using OrderService.Domain;

namespace OrderService.Context
{
    public class OrderProcessingSystemDbContext :DbContext
    {
        public OrderProcessingSystemDbContext( DbContextOptions dbContextOptions ): base(dbContextOptions) { 
        }

        public DbSet<Product> Product { get; set; }

        public DbSet<Order> Order { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .Property(o => o.TotalAmount)
                .HasPrecision(18, 2); // Adjust precision and scale according to your requirements

            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasPrecision(18, 2); // Adjust precision and scale according to your requirements
        }
    }
}
