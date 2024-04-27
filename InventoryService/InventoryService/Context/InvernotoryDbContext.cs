using InventoryService.Domain;
using Microsoft.EntityFrameworkCore;

namespace InventoryService.Context
{
    public class InvernotoryDbContext:DbContext
    {
        public InvernotoryDbContext(DbContextOptions<InvernotoryDbContext> dbContextOptions) : base(dbContextOptions)
        {
        }

        public DbSet<Inventory> Inventory { get; set; }
    }
}
