using OrderService.Domain;
using OrderService.Dtos.ProductDtos;
using OrderService.RepositoryServices.Interface;
using OrderService.RepositoryServices.Repository;
using OrderService.ServiceLayer.IServices;
using System.ComponentModel.DataAnnotations;

namespace OrderService.ServiceLayer.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await _productRepository.GetAll();
        }

        public async Task<Product> AddProduct(AddProductDto productDto)
        {
            // Validate the product DTO
            var validationContext = new ValidationContext(productDto, serviceProvider: null, items: null);
            var validationResults = new List<ValidationResult>();

            if (!Validator.TryValidateObject(productDto, validationContext, validationResults, validateAllProperties: true))
            {
                var validationErrors = string.Join("; ", validationResults.Select(r => r.ErrorMessage));
                throw new ValidationException(validationErrors);
            }
            
            // Create a new product entity
            var addProd = new Product
            {
                ProductId = Guid.NewGuid(),
                ProductName = productDto.ProductName,
                ProductDescription = productDto.ProductDescription,
                ProductCategory = productDto.ProductCategory,
                Price = productDto.Price,
                CreatedBy = Guid.Parse("68FBC4E3-541B-4D7D-A58F-2931B35CAE33"),
                CreatedOn = DateTime.Now,
                UpdatedBy = Guid.Parse("68FBC4E3-541B-4D7D-A58F-2931B35CAE33"),
                UpdatedOn = DateTime.Now
            };
            // Use the InventoryServiceClient for the gRPC call
            var inventoryServiceClient = InventoryServiceClient.Instance;
            var reply = await inventoryServiceClient.AddNewInventoryDetailsAsync(addProd.ProductId, addProd.ProductName, productDto.Quantity);

            if (reply)
            {
                // Add the product to the repository
                var addedProduct = await _productRepository.Add(addProd);
                await _productRepository.Save();
                return addedProduct;
            }
            else
            {
                throw new Exception("Failed to add inventory details.");
            }
        }

        public async Task<GetProductDto> GetProductById(Guid? id)
        {
            var product= new GetProductDto();
            if(id != null)
            {
                var inventoryServiceClient = InventoryServiceClient.Instance;
                var reply = await inventoryServiceClient.GetInventoryDetailsByIdAsync((Guid)id);
                if (reply!= null)
                {
                    var prodRes=await _productRepository.GetById((Guid)id);
                    product.ProductName = prodRes.ProductName;
                    product.ProductId = (Guid)id;
                    product.Price = prodRes.Price;
                    product.ProductCategory = prodRes.ProductCategory;
                    product.ProductDescription = prodRes.ProductDescription;
                    product.Quantity=reply.Quantity;
                }
            }

            return product;
        }
    }
}
