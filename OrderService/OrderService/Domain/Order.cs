using System.ComponentModel.DataAnnotations.Schema;

namespace OrderService.Domain
{
    public class Order
    {
        public Guid OrderId { get; set; }
        public decimal TotalAmount { get; set; }
        public int Quantity {  get; set; }

        public Guid CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public Guid UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; } = DateTime.Now;
        // Foreign key for Product
        [ForeignKey("Product")]
        public Guid ProductId { get; set; }

        // Navigation property for Product
        public Product? Product { get; set; }
    }
}
