using Amedia.Domain;
using Amedia.Logic;
using Amedia.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Amedia.Areas.Users.Pages
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public UserVM User { get; set; }
        public IEnumerable<SelectListItem> Roles { get; set; }

        private readonly UsersLogic _userLogic;

        public CreateModel(UsersLogic userLogic)
        {
            _userLogic = userLogic;
        }

        public void OnGet()
        {
            var roles = _userLogic.GetAllRoles();

            Roles = roles.Select(r => new SelectListItem
            {
                Text = r.Name,
                Value = r.Id.ToString()
            }).ToList();
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
