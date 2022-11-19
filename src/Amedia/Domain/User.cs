using Amedia.DataAccess;

namespace Amedia.Domain
{
    public class User : BaseEntity
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public int RoleId { get; set; }
        public UserRole Role { get; set; }
    }
}
