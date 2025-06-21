using InventoryApi.Data;
using InventoryApi.Models;
using InventoryApi.Models.Entities;

namespace InventoryApi.Service
{
    public class ItemService : IItemService
    {
        private readonly ApplicationDbContext dbContext;

        public ItemService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<Item> GetAllItems()
        {
            return dbContext.Items.ToList();
        }

        public Item GetItemById(Guid id)
        {
            return dbContext.Items.Find(id);
        }

        public Item AddItem(AddItemDto addItemDto)
        {

            if (string.IsNullOrWhiteSpace(addItemDto.Name))
                throw new ArgumentException("Name is required.");

            if (addItemDto.Price <= 0)
                throw new ArgumentException("Price must be greater than zero.");

            if (string.IsNullOrWhiteSpace(addItemDto.ItemType))
                throw new ArgumentException("Item Type is required.");
            var item = new Item
            {
                Name = addItemDto.Name,
                Description = addItemDto.Description,
                Price = addItemDto.Price,
                ItemType = addItemDto.ItemType
            };

            dbContext.Items.Add(item);
            dbContext.SaveChanges();

            return item;
        }


        public Item UpdateItem(Guid id, UpdateitemDto updateItemDto)
        {

            if (string.IsNullOrWhiteSpace(updateItemDto.Name))
                throw new ArgumentException("Name is required.");

            if (updateItemDto.Price <= 0)
                throw new ArgumentException("Price must be greater than zero.");

            if (string.IsNullOrWhiteSpace(updateItemDto.ItemType))
                throw new ArgumentException("Item Type is required.");


            var item = dbContext.Items.Find(id);
            if (item == null)
                return null;

            item.Name = updateItemDto.Name;
            item.Description = updateItemDto.Description;
            item.Price = updateItemDto.Price;
            item.ItemType = updateItemDto.ItemType;

            dbContext.SaveChanges();
            return item;
        }

        public bool DeleteItem(Guid id)
        {
            var item = dbContext.Items.Find(id);
            if (item == null)
                return false;

            dbContext.Items.Remove(item);
            dbContext.SaveChanges();
            return true;
        }

    }
}
