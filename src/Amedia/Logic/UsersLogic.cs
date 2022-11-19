using Amedia.DataAccess;
using Amedia.Domain;
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
    }
}
