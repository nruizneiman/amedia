using Amedia.Logic;
using Amedia.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Amedia.Areas.Users.Pages
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public UserVM User { get; set; }

        private readonly UsersLogic _userLogic;

        public CreateModel(UsersLogic userLogic)
        {
            _userLogic = userLogic;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                _userLogic.CreateUser(User);
            }
            catch (Exception)
            {
                return Page();
            }

            return RedirectToPage("Management");
        }
    }
}
