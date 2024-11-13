
using Handmade.Models;
using Handmade.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Handmade.Controllers
{
    public class UserController : Controller
    {
        private readonly DataDbContext context;
        private readonly UserManager<ApplicationUser> userManager;
        public UserController(DataDbContext context, UserManager<ApplicationUser> userManager)
        {
            this.context = context; // استخدم Dependency Injection
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            var users = context._Users.ToList(); // احصل على جميع المستخدمين
            return View(users); // مرر المستخدمين إلى العرض
        }

        public IActionResult Showuser(string id)
        {
            var D1 = userManager.Users.FirstOrDefault(d => d.Id == id);
            if (D1 == null) // تحقق من وجود المستخدم
            {
                return NotFound(); // إذا لم يكن موجودًا، ارجع خطأ 404
            }
            return View("Showuser", D1); // مرر المستخدم إلى العرض
        }

        public IActionResult Edit(string id)
        {
            var D1 = userManager.Users.FirstOrDefault(d => d.Id == id);
            return View("Edit", D1);
        }
        public async Task<IActionResult> saveaEditdep(string id, RegisterUserViewModel user, IFormFile ImageUrl)
        {
            if (user.Name != null)
            {
                var userDb = userManager.Users.FirstOrDefault(d => d.Id == id);

                if (ImageUrl != null && ImageUrl.Length > 0)
                {
                    // تحديد المسار للصورة الجديدة
                    var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", ImageUrl.FileName);
                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        await ImageUrl.CopyToAsync(stream);
                    }

                    // تحديث مسار الصورة في النموذج
                    userDb.imageurl = "/images/" + ImageUrl.FileName;
                }
                else
                {
                    // إذا لم يتم رفع صورة جديدة، احتفظ بالصورة القديمة
                    userDb.imageurl = userDb.imageurl;
                }

                // تحديث باقي الخصائص
                userDb.Name = user.Name;
                userDb.Email = user.Email;
                userDb.imageurl = user.imageurl;
                

                await context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            return View("Edit", user);
        }




        public async Task<IActionResult> Delete(string id)
        {
            var D1 = userManager.Users.FirstOrDefault(d => d.Id == id);
           await userManager.DeleteAsync(D1);
            context.SaveChanges();
            return RedirectToAction("listusers", "Dashboard");
        }
    }
}
