using InventoryApi.Models;
using InventoryApi.Service;
using Microsoft.AspNetCore.Mvc;

namespace InventoryApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService userService;

        public UserController(UserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            return Ok(userService.GetUsers());
        }

        [HttpGet("{id}")]

        public IActionResult GetUsersById(Guid id)
        {
            var user = userService.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);

        }

        [HttpPost]

        public IActionResult AddUsers(AddUserDto addUserDto)
        {
            var createdUser = userService.AddUser(addUserDto);
            return Ok(createdUser);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult UpdateUsers(Guid id, UpdateUserDto updateUserDto)
        {
            var updatedUser = userService.UpdateUser(id, updateUserDto);
            if (updatedUser == null)
                return NotFound();

            return Ok(updatedUser);
        }

        

        [HttpDelete]
        [Route("{id:guid}")]

        public IActionResult DeleteItems(Guid id)
        {
            var result = userService.DeleteUser(id);
            if (!result)
                return NotFound();

            return Ok();
        }
    }
}
