using Amedia.Domain;
using Amedia.Logic;
using Amedia.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Amedia.Areas.Users.Pages
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public UserVM User { get; set; }

        private readonly UsersLogic _userLogic;

        public EditModel(UsersLogic userLogic)
        {
            _userLogic = userLogic;
        }

        public void OnGet(int id)
        {
            var user = _userLogic.GetUserDetails(id);

            User = new UserVM
            {
                Id = user.Id,
                UserName = user.UserName,
                RoleId = user.RoleId
            };
        }

        public async Task<IActionResult> OnPostAsync()
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
