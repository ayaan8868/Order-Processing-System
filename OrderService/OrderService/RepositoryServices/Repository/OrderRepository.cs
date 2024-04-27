using OrderService.Context;
using OrderService.Domain;
using OrderService.RepositoryServices.Interface;

namespace OrderService.RepositoryServices.Repository
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(OrderProcessingSystemDbContext context) : base(context)
        {
        }

        public async Task<Order> AddOrder(Order order)
        {
            await Add(order);
            return order;
        }
    }
}
