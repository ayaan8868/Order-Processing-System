using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderService.Domain;
using OrderService.Dtos.ProductDtos;
using OrderService.RepositoryServices.Interface;
using OrderService.ServiceLayer.IServices;
using System.Text.RegularExpressions;

namespace OrderService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productService;
        public ProductController(ILogger<ProductController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var products = await _productService.GetAllProducts();
                return Ok(products);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving products: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving products");
            }
        }
        [Route("{id:Guid}")]
        [HttpGet]
        public async Task<IActionResult>Get(Guid? id)
        {
            try
            {
                var resposne = await _productService.GetProductById(id);
                return (resposne != null) ? Ok(resposne) : BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the GET request.");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message+"Error retrieving the products");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddProductDto product)
        {
            if (product == null)
            {
                return BadRequest("Product object is null");
            }

            try
            {
                var addedProduct = await _productService.AddProduct(product);
                return CreatedAtAction(nameof(Post), addedProduct);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error adding product: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error adding product");
            }
        }
    }
}
