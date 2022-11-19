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

        public DeleteModel(UsersLogic userLogic)
        {
            _userLogic = userLogic;
        }

        public void OnGet(int id)
        {
            User = _userLogic.GetUserDetails(id);
        }

        public async Task<IActionResult> OnPost(int id)
        {
            _userLogic.DeleteUser(id);

            return RedirectToPage("Management");
        }
    }
}
