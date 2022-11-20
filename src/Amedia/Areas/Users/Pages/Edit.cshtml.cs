using Amedia.Domain;
using Amedia.Logic;
using Amedia.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Amedia.Areas.Users.Pages
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public UserVM User { get; set; }
        public IEnumerable<SelectListItem> Roles { get; set; }

        private readonly UsersLogic _userLogic;
        private readonly AuthLogic _authLogic;

        public EditModel(UsersLogic userLogic, AuthLogic authLogic)
        {
            _userLogic = userLogic;
            _authLogic = authLogic;
        }

        public async Task<IActionResult> OnGet(int id)
        {
            var loggedInUser = HttpContext.Session.GetString("LoggedInUserId");
            if (!_authLogic.UserHasAccess(loggedInUser)) return Redirect("~/");

            var user = _userLogic.GetUserDetails(id);

            User = new UserVM
            {
                Id = user.Id,
                UserName = user.UserName,
                RoleId = user.RoleId
            };

            var roles = _userLogic.GetAllRoles();

            Roles = roles.Select(r => new SelectListItem
            {
                Text = r.Name,
                Value = r.Id.ToString(),
                Selected = r.Id == user.RoleId
            }).ToList();

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            try
            {
                _userLogic.UpdateUser(User);
            }
            catch (Exception)
            {
                return Page();
            }

            return RedirectToPage("Management");
        }
    }
}
