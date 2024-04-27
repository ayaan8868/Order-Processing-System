using OrderService.Domain;

namespace OrderService.RepositoryServices.Interface
{
    public interface IOrderRepository :IGenericRepository<Order>
    {
        Task<Order> AddOrder(Order order);
    }


}
