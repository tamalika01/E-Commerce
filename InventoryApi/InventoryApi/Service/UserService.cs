using InventoryApi.Data;
using InventoryApi.Models;
using InventoryApi.Models.Entities;

namespace InventoryApi.Service
{
    public class UserService
    {

        public readonly ApplicationDbContext dbContext;

        public UserService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<User> GetUsers()
        {
            return dbContext.Users.ToList();
        }

        public User GetUserById(Guid id)
        {
            return dbContext.Users.Find(id);
        }

        public User AddUser(AddUserDto dto)
        {
            if(string.IsNullOrWhiteSpace(dto.Name))
            {
                throw new ArgumentException("User Name is required.");
            }

            if (string.IsNullOrWhiteSpace(dto.Email))
            {
                throw new ArgumentException("User email is required.");
            }

            var user = new User()
            {
                Name = dto.Name,
                Email = dto.Email,
                Role = dto.Role,
            };

            dbContext.Users.Add(user);
            dbContext.SaveChanges();

            return user;

        }

        public User UpdateUser(Guid id, UpdateUserDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
            {
                throw new ArgumentException("User Name is required.");
            }

            if (string.IsNullOrWhiteSpace(dto.Email))
            {
                throw new ArgumentException("User email is required.");
            }

            var user = dbContext.Users.Find(id);
            if(user == null)
            {
                return null;
            }
            user.Name = dto.Name;
            user.Email = dto.Email;
            user.Role = dto.Role;

            dbContext.SaveChanges();
            return user;

        }

        public bool DeleteUser(Guid id)
        {
            var user = dbContext.Users.Find(id);
            if(user == null)
            {
                return false;

            }

            dbContext.Users.Remove(user);
            dbContext.SaveChanges();
            return true;
        }

    }

}