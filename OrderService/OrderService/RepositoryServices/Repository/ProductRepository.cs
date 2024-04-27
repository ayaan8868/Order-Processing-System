using OrderService.Context;
using OrderService.Domain;
using OrderService.RepositoryServices.Interface;

namespace OrderService.RepositoryServices.Repository
{
    public class ProductRepository : GenericRepository<Product>,IProductRepository
    {
        public ProductRepository(OrderProcessingSystemDbContext context) : base(context)
        {
        }
        
    }
}
