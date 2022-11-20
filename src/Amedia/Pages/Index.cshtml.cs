using Amedia.Logic;
using Amedia.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Amedia.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public LoginVM Login { get; set; }

        private readonly ILogger<IndexModel> _logger;
        private readonly UsersLogic _usersLogic;
        private readonly AuthLogic _authLogic;

        public IndexModel(ILogger<IndexModel> logger, UsersLogic usersLogic, AuthLogic authLogic)
        {
            _logger = logger;
            _usersLogic = usersLogic;
            _authLogic = authLogic;
        }

        public async Task<IActionResult> OnGet()
        {
            var loggedInUser = HttpContext.Session.GetString("LoggedInUserId");

            if (!string.IsNullOrWhiteSpace(loggedInUser))
            {
                // The user is already logged in
                var idWasParsed = int.TryParse(loggedInUser, out var loggedInUserId);
                if (loggedInUserId == null || !idWasParsed) return Page(); // Something really weird has happened.

                var user = _usersLogic.GetUserDetails(loggedInUserId);
                return Redirect(user.Role.RelativeStartupPagePath);
            }

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            var verificationResult = _authLogic.ValidateLoginCredentials(Login);

            if (verificationResult == null)
            {
                ViewData["Message"] = "Please verify your credentials, the combination specified is not valid.";
                return Page();
            }

            switch(verificationResult.VerificationResult)
            {
                case PasswordVerificationResult.Success:
                    HttpContext.Session.SetString("LoggedInUserId", verificationResult.User.Id.ToString());
                    return Redirect(verificationResult.User.Role.RelativeStartupPagePath);
                default:
                    ViewData["Message"] = "Please verify your credentials, the combination specified is not valid.";
                    return Page();
            }
        }
    }
}