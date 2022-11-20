using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Amedia.Areas.Users.Pages
{
    public class LogoutModel : PageModel
    {
        public void OnGet() { }

        public IActionResult OnPostAsync()
        {
            HttpContext.Session.Clear();
            return Redirect("~/");
            //return RedirectToPage("Management");
            //return RedirectToPage("./Index");
            //return RedirectToAction("Index", "Home");
        }
    }
}
