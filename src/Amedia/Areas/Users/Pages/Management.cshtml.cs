using Amedia.Domain;
using Amedia.Logic;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Amedia.Areas.Users.Pages
{
    public class ManagementModel : PageModel
    {
        private UsersLogic _logic;

        public IEnumerable<User> Users { get; set; }

        public ManagementModel(UsersLogic logic)
        {
            _logic = logic;
        }

        public void OnGet()
        {
            Users = _logic.GetAllUsers();
        }
    }
}
