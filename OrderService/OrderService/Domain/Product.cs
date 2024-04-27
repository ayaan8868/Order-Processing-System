using System.ComponentModel.DataAnnotations;

namespace OrderService.Domain
{
    public class Product
    {
        [Key]
        public Guid ProductId { get; set; }
        public string? ProductName { get; set; }
        public string ProductDescription { get; set; } = string.Empty;
        public string ProductCategory { get; set; } =string.Empty;
        public decimal? Price { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedOn { get; set;}=DateTime.Now;
        public Guid UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set;} = DateTime.Now;
    }
}
