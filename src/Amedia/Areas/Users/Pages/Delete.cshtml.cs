using Amedia.Domain;
using Amedia.Logic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Amedia.Areas.Users.Pages
{
    public class DeleteModel : PageModel
    {
        public User User { get; set; }

        private readonly UsersLogic _userLogic;
        private readonly AuthLogic _authLogic;

        public DeleteModel(UsersLogic userLogic, AuthLogic authLogic)
        {
            _userLogic = userLogic;
            _authLogic = authLogic;
        }

        public async Task<IActionResult> OnGet(int id)
        {
            var loggedInUser = HttpContext.Session.GetString("LoggedInUserId");
            if (!_authLogic.UserHasAccess(loggedInUser)) return Redirect("~/");

            User = _userLogic.GetUserDetails(id);

            return Page();
        }

        public async Task<IActionResult> OnPost(int id)
        {
            _userLogic.DeleteUser(id);

            return RedirectToPage("Management");
        }
    }
}
