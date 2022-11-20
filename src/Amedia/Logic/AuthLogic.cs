using Amedia.DataAccess;
using Amedia.Helpers;
using Amedia.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Amedia.Logic
{
    public class AuthLogic
    {
        private AmediaContext _context;

        public AuthLogic(AmediaContext context)
        {
            _context = context;
        }

        public AuthResult ValidateLoginCredentials(LoginVM login)
        {
            var user = _context.Users
                .Include(u => u.Role)
                .FirstOrDefault(u => u.UserName == login.UserName);

            if (user == null)
            {
                return new AuthResult
                {
                    VerificationResult = PasswordVerificationResult.Failed
                };
            }

            // Validate Password
            var passwordVerificationResult = SecurityHelper.ValidatePassword(user.Password, login.Password);

            if (passwordVerificationResult == PasswordVerificationResult.Success)
            {
                return new AuthResult
                {
                    VerificationResult = passwordVerificationResult,
                    User = user
                };
            }

            // Weird, but possible
            return null;
        }

        public bool UserHasAccess(string? loggedInUser)
        {
            if (!string.IsNullOrWhiteSpace(loggedInUser))
            {
                // The user is already logged in
                var idWasParsed = int.TryParse(loggedInUser, out var loggedInUserId);

                if (idWasParsed)
                {
                    return _context.Users
                        .Include(u => u.Role)
                        .FirstOrDefault(u => u.Id == loggedInUserId).RoleId == 1;
                }
            }

            return false;
        }
    }
}
