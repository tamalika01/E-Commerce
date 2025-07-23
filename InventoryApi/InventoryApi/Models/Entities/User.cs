namespace InventoryApi.Models.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public  string? Role { get; set; }

        public required string Password { get; set; }
    }
}
