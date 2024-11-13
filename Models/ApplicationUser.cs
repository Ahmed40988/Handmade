using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Handmade.Models
{
    public class ApplicationUser:IdentityUser
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        public char Gender { get; set; }
        //public string Phone { get; set; }

        public string imageurl { get; set; }

        public int? RoleId { get; set; }
    }
}
