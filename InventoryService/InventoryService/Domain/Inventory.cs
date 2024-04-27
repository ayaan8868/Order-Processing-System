using System.ComponentModel.DataAnnotations;

namespace InventoryService.Domain
{
    public class Inventory
    {
        [Key]
        public Guid InventoryId {  get; set; }
        [Required]
        public Guid ProductId { get; set; }
        [Required]
        public string? ProductName { get; set; }
        [Required]
        public int Quantity {  get; set; }
    }
}
