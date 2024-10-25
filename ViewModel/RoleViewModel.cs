using System.ComponentModel.DataAnnotations;

namespace Handmade.ViewModel
{
	public class RoleViewModel
	{
		[Required]
		public string RoleName { get; set; }
	}
}
