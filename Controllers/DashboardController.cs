using Handmade.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Handmade.Controllers
{
    [Authorize(Roles = "admin")]
    public class DashboardController : Controller
    {
        private readonly DataDbContext _context;
        private readonly UserManager<ApplicationUser> userManager;

        public DashboardController(DataDbContext context,UserManager <ApplicationUser> userManager)
        {
            _context = context;
            this.userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult listproducts()
        {
            var productlist=_context.Products.ToList();
            return View(productlist);
        }
        public async Task<IActionResult >listusers()
        {
            var listusers = await userManager.Users.ToListAsync();
            return View(listusers);
        }

        public IActionResult listcategore()
        {
            var listcategore = _context.Categories.ToList();

            return View(listcategore);
        } 
        public IActionResult listorder()
        {
            var listorder = _context.Orders.ToList();
            return View(listorder);
        }

      
    }
}
