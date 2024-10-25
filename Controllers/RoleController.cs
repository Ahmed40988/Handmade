using Handmade.Migrations;
using Handmade.Models;
using Handmade.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Handmade.Controllers
{
    [Authorize(Roles = "admin")]
    public class RoleController : Controller
    {
		private readonly RoleManager<IdentityRole> roleManager;

		public RoleController(RoleManager<IdentityRole> roleManager)
		{
			this.roleManager = roleManager;
		}

		public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult New()
        {
            return View();
        }
        [HttpPost]

        public async Task<IActionResult> New(RoleViewModel roleViewModel)
        {
            if (ModelState.IsValid)
            {
                //save in DB
                IdentityRole rolemodel = new IdentityRole();
                rolemodel.Name=roleViewModel.RoleName;
             IdentityResult result=  await roleManager.CreateAsync(rolemodel);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);

                    }

                }
            }

            return View(roleViewModel);
        } 
    }
}
