using InventoryApi.Models;
using InventoryApi.Models.Entities;
using InventoryApi.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace InventoryApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService userService;
        private readonly IConfiguration _configuration;

        public UserController(IConfiguration configuration, UserService userService)
        {
            _configuration = configuration;
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
        
        public IActionResult DeleteUsers(Guid id)
        {
            try
            {
                var result = userService.DeleteUser(id);
                if (!result)
                    return NotFound();

                return Ok("User deleted");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }


        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto loginDto)
        {
            var user = userService.GetUserByEmailAndPassword(loginDto.Email, loginDto.Password);

            if (user == null)
                return Unauthorized("Invalid credentials");

            var token = GenerateJwtToken(user);
            return Ok(new { token });
        }

        private string GenerateJwtToken(User user)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");

            var claims = new[]
            {
               new Claim(JwtRegisteredClaimNames.Sub, user.Email),
               new Claim(ClaimTypes.Role, user.Role ?? "User"),
               new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["DurationInMinutes"])),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        //[Authorize(Roles = "ADMIN")]
        //[HttpDelete("{id}")]
        //public IActionResult Delete(Guid id)
        //{
        //    var success = userService.DeleteUser(id);
        //    if (!success)
        //        return NotFound();

        //    return Ok("User deleted successfully");
        //}
    }
}
