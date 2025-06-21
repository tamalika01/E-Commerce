using InventoryApi.Data;
using InventoryApi.Models;
using InventoryApi.Models.Entities;
using InventoryApi.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItemService itemService;
        public ItemController(IItemService itemService)
        {
            this.itemService = itemService;
        }

        [HttpGet]
        public IActionResult GetItems()
        {
            var items = itemService.GetAllItems();
            return Ok(items);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetItemById(Guid id)
        {
            var item = itemService.GetItemById(id);
            if (item == null)
                return NotFound();

            return Ok(item);
        }

        [HttpPost]
        public IActionResult AddItems(AddItemDto addItemDto)
        {
            var createdItem = itemService.AddItem(addItemDto);
            return Ok(createdItem);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult UpdateItems(Guid id, UpdateitemDto updateitemDto)
        {
            var updatedItem = itemService.UpdateItem(id, updateitemDto);
            if (updatedItem == null)
                return NotFound();

            return Ok(updatedItem);
        }

        [HttpDelete]
        [Route("{id:guid}")]

        public IActionResult DeleteItems(Guid id)
        {
            var result = itemService.DeleteItem(id);
            if (!result)
                return NotFound();

            return Ok();
        }


    }
}
