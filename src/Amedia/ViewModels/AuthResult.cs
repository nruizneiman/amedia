using Amedia.Domain;
using Microsoft.AspNetCore.Identity;

namespace Amedia.ViewModels
{
    public class AuthResult
    {
        public PasswordVerificationResult VerificationResult { get; set; }
        public User? User { get; set; }
    }
}
