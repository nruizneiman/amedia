using Amedia.DataAccess;

namespace Amedia.Domain
{
    public class UserRole : BaseEntity
    {
        public string Name { get; set; }

        public string RelativeStartupPagePath { get; set; }

        public virtual IEnumerable<User> User { get; set; }
    }
}
