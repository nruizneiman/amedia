using System.ComponentModel.DataAnnotations;

namespace Amedia.ViewModels
{
    public class LoginVM
    {
        public string UserName { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
