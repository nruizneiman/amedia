using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;

namespace Amedia.UnitTests
{
    public class SecurityHelperTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCase("uQH+gjQPS@Uu2Rda", "uQH+gjQPS@Uu2Rda")]
        public void HashPasswordV2(string pass1, string pass2)
        {
            var hashedPassword = new PasswordHasher<object?>().HashPassword(null, pass1);

            var passwordVerificationResult = new PasswordHasher<object?>().VerifyHashedPassword(null, hashedPassword, pass2);
            switch (passwordVerificationResult)
            {
                case PasswordVerificationResult.Failed:
                    Console.WriteLine("Password incorrect.");
                    break;

                case PasswordVerificationResult.Success:
                    Console.WriteLine("Password ok.");
                    break;

                case PasswordVerificationResult.SuccessRehashNeeded:
                    Console.WriteLine("Password ok but should be rehashed and updated.");
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            Assert.AreEqual(PasswordVerificationResult.Success, passwordVerificationResult);
        }
    }
}