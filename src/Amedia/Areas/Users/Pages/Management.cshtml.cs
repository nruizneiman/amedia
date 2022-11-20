using Amedia.Domain;
using Amedia.Logic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Amedia.Areas.Users.Pages
{
    public class ManagementModel : PageModel
    {
        private readonly UsersLogic _userLogic;
        private readonly AuthLogic _authLogic;

        public IEnumerable<User> Users { get; set; }

        public ManagementModel(UsersLogic userLogic, AuthLogic authLogic)
        {
            _userLogic = userLogic;
            _authLogic = authLogic;
        }

        public async Task<IActionResult> OnGet()
        {
            var loggedInUser = HttpContext.Session.GetString("LoggedInUserId");
            if (!_authLogic.UserHasAccess(loggedInUser)) return Redirect("~/");

            Users = _userLogic.GetAllUsers();
            return Page();
        }
    }
}
