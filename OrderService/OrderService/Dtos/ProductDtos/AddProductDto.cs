using System.ComponentModel.DataAnnotations;

namespace OrderService.Dtos.ProductDtos
{
    public class AddProductDto
    {
        [Required]
        public string? ProductName { get; set; }
        public string ProductDescription { get; set; } = string.Empty;
        [Required]
        public string ProductCategory { get; set; } = string.Empty;
        [Required]
        public decimal? Price { get; set; }
        [Required]
        public int Quantity { get; set; }

    }
}
