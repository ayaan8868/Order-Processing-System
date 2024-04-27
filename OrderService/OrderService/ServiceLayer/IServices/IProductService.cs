using OrderService.Domain;
using OrderService.Dtos.ProductDtos;

namespace OrderService.ServiceLayer.IServices
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProducts();
        Task<Product> AddProduct(AddProductDto productDto);
        Task<GetProductDto> GetProductById(Guid? id);
    }
}
