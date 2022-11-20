using Amedia.DataAccess;
using Amedia.Domain;
using Amedia.Helpers;
using Amedia.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Amedia.Logic
{
    public class UsersLogic
    {
        private AmediaContext _context;

        public UsersLogic(AmediaContext context)
        {
            _context= context;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _context.Users.Include(u => u.Role);
        }

        public void CreateUser(UserVM user)
        {
            if (user == null || string.IsNullOrWhiteSpace(user.UserName) || string.IsNullOrWhiteSpace(user.Password)) { throw new Exception("User data is not valid."); }

            // Assuming user's data is already validated
            var newUser = new User
            {
                UserName = user.UserName,
                Password = SecurityHelper.HashPassword(user.Password),
                RoleId = user.RoleId != 0 ? user.RoleId : 2 // If for some reason the Role was not defined, we default to 2 (Guest)
            };

            _context.Users.Add(newUser);
            _context.SaveChanges();
        }

        public User GetUserDetails(int id)
        {
            return _context.Users.Include(u => u.Role).FirstOrDefault(u => u.Id == id);
        }

        public void DeleteUser(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            _context.Users.Remove(user);
            _context.SaveChanges();
        }

        public void UpdateUser(UserVM user)
        {
            var existingUser = _context.Users.FirstOrDefault(u =>u.Id == user.Id);

            if (existingUser != null)
            {
                _context.Users.Update(existingUser);
                existingUser.RoleId = user.RoleId;
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("User not found");
            }
        }

        public IEnumerable<UserRole> GetAllRoles()
        {
            return _context.UserRoles;
        }
    }
}
