namespace InventoryApi.Models.Entities
{
    public class Item
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }

        public required string ItemType {  get; set; }
        public string? Description { get; set; }
        public required decimal  Price { get; set; }
    }
}
