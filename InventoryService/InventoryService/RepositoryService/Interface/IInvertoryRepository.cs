using InventoryService.Domain;

namespace InventoryService.RepositoryService.Interface
{
    public interface IInvertoryRepository:IGenericRepository<Inventory>
    {
        Task<Inventory> AddInventory(Inventory inventory);
        Inventory UpdateInVentory(Inventory inventory);
        Task<Inventory> GetInventoryDetailsByProductId(Guid id);
    }
}
