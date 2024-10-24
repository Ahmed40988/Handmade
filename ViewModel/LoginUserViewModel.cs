using Handmade.Models;
using System.ComponentModel.DataAnnotations;

namespace Handmade.ViewModel
{
    public class LoginUserViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [Unique("Password")]
        public string Password { get; set; }
         
        public bool RememberMe { get; set; }

    }
}
