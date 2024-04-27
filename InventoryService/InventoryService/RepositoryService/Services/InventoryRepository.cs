using InventoryService.Context;
using InventoryService.Domain;
using InventoryService.RepositoryService.Interface;

namespace InventoryService.RepositoryService.Services
{
    public class InventoryRepository : GenericRepository<Inventory>, IInvertoryRepository
    {
        public InventoryRepository(InvernotoryDbContext context) : base(context)
        {
        }

        public async Task<Inventory> AddInventory(Inventory inventory)
        {
           await Add(inventory);
           return inventory;
        }

        public Inventory UpdateInVentory(Inventory inventory)
        {
            Update(inventory);
            return inventory;
        }

        public  async Task<Inventory> GetInventoryDetailsByProductId(Guid id)
        {
            var res = await GetAll();
            var prodRes=res.FirstOrDefault(x => x.ProductId == id);
            return prodRes;
        }
    }
}
