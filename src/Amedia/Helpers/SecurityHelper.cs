using Microsoft.AspNetCore.Identity;

namespace Amedia.Helpers
{
    public class SecurityHelper
    {
        public static string HashPassword(string password)
        {
            var hashedPassword = new PasswordHasher<object?>().HashPassword(null, password);

            return hashedPassword;
        }
    }
}
