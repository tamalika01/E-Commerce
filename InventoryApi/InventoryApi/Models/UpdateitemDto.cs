using System.ComponentModel.DataAnnotations;

namespace InventoryApi.Models
{
    public class UpdateitemDto
    {
        [Required(ErrorMessage = "Name is required.")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "Item Type is required.")]
        public required string ItemType { get; set; }
        public string? Description { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
        public required decimal Price { get; set; }
    }
}
