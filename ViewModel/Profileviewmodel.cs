using Handmade.Models;
using System.ComponentModel.DataAnnotations;

namespace Handmade.ViewModel
{
    public class Profileviewmodel
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        public string Username { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [Unique("Email")]
        public string Email { get; set; }
        public string Phone { get; set; }
        public string? imageurl { get; set; }

        public string? Rolename { get; set; }

    }
}
