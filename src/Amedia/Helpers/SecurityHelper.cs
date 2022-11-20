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

        public static PasswordVerificationResult ValidatePassword(string hashedPassword, string password)
        {
            var passwordVerificationResult = new PasswordHasher<object?>().VerifyHashedPassword(null, hashedPassword, password);

            return passwordVerificationResult;
        }
    }
}
