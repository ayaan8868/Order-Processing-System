using System.ComponentModel.DataAnnotations;

namespace OrderService.Dtos.ProductDtos
{
    public class GetProductDto
    {
        [Required]
        public Guid ProductId { get; set; }
        [Required]
        public string? ProductName { get; set; }
        public string? ProductDescription { get; set; }
        [Required]
        public string? ProductCategory { get; set; } 
        [Required]
        public decimal? Price { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}
