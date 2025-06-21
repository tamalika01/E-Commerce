using InventoryApi.Models.Entities;
using InventoryApi.Models;

namespace InventoryApi.Service
{
    public interface IItemService
    {
        
            List<Item> GetAllItems();
            Item GetItemById(Guid id);
            Item AddItem(AddItemDto addItemDto);
            Item UpdateItem(Guid id, UpdateitemDto updateItemDto);
            bool DeleteItem(Guid id);
        
    }
}
