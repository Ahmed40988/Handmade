using Handmade.Models;
using Handmade.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Handmade.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
public async Task<IActionResult> Register(RegisterUserViewModel newUserVM, IFormFile imageurl)
        {
            // Ensure server-side validation is met
            if (newUserVM.Name!=null)
            {
                // Create a new ApplicationUser object
                ApplicationUser userModel = new ApplicationUser
                {
                    Name = newUserVM.Name,
                    UserName = newUserVM.Username, // Create a unique username
                    Email = newUserVM.Email,
                    Gender = newUserVM.Gender,
                    PhoneNumber = newUserVM.Phone,  // Add Phone number as per the model
                };

                // Handle image upload
                if (imageurl != null && imageurl.Length > 0)
                {
                    // Ensure a unique file name to avoid overwriting
                    var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(imageurl.FileName);

                    // Determine the path to save the image in wwwroot/images folder
                    var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", uniqueFileName);
                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        await imageurl.CopyToAsync(stream);
                    }

                    // Store the image path in the ApplicationUser object
                    userModel.imageurl = "/images/" + uniqueFileName;  // Relative URL to the image
                }

                // Create the user with the UserManager and set the password
                IdentityResult result = await userManager.CreateAsync(userModel, newUserVM.Password);

                // Check if the user creation was successful
                if (result.Succeeded)
                {
                    //create Role Default "user"
                    await userManager.AddToRoleAsync(userModel, "User");
                    //create Cookie
                    await signInManager.SignInAsync(userModel, isPersistent: false);
                    // Redirect to Home after successful registration
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    // Add errors to ModelState to display on the form
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            // If ModelState is invalid, redisplay the form with validation errors
            return View(newUserVM);
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        } 
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginUserViewModel revemodel)
        {
            if (ModelState.IsValid)
            {
               ApplicationUser userModel= await userManager.FindByNameAsync(revemodel.Username);
              
                if (userModel != null)
                {
                    bool found = await userManager.CheckPasswordAsync(userModel, revemodel.Password);
                    if (found)
                    {
                        await signInManager.SignInAsync(userModel, revemodel.RememberMe);
                        return RedirectToAction("Index", "Home");
                    }

                }
                ModelState.AddModelError("", "Username Or Password Wrong");
            }
            return View(revemodel);
        }
        public IActionResult Logout()
        {
            // remove cookie
            signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Profile()
        {
           
            var userId = userManager.GetUserId(User); 
            var user= await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return RedirectToAction("Login");
            }
            var roles = await userManager.GetRolesAsync(user);
            Profileviewmodel profileviewmodel = new Profileviewmodel();
            profileviewmodel.Name = user.Name;
            profileviewmodel.Username = user.UserName;
            profileviewmodel.Email = user.Email;
            profileviewmodel.Phone = user.PhoneNumber;
            profileviewmodel.imageurl = user.imageurl;
            profileviewmodel.Rolename = roles.FirstOrDefault();

            return View(profileviewmodel);

        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> EditProfile()
        {
            var user = await userManager.GetUserAsync(User); // جلب المستخدم الحالي

            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            // تهيئة ViewModel ببيانات المستخدم الحالي
            var model = new Profileviewmodel
            {
                Name = user.Name,
                Username = user.UserName,
                Email = user.Email,
                Phone = user.PhoneNumber,
                imageurl = user.imageurl

            };

            return View(model); // تمرير البيانات إلى النموذج
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProfile(Profileviewmodel model, IFormFile newImage)
        {
            if (model.Name!=null)
            {
                var user = await userManager.GetUserAsync(User); // جلب المستخدم الحالي

                if (user == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                // تحديث بيانات المستخدم
                user.Name = model.Name;
                user.Email = model.Email;
                user.PhoneNumber = model.Phone;
                user.UserName = model.Username;



                // التعامل مع تحديث الصورة في حالة اختيار صورة جديدة
                if (newImage != null && newImage.Length > 0)
                {
                    var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(newImage.FileName);
                    var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", uniqueFileName);

                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        await newImage.CopyToAsync(stream);
                    }

                    user.imageurl = "/images/" + uniqueFileName;
                }
                else
                {
                  // if don't update image take old image
                    user.imageurl = user.imageurl;
                }
                var result = await userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    TempData["Message"] = "Profile updated successfully!";
                    return RedirectToAction("Profile");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }




        [HttpGet]
        [Authorize(Roles="admin")]
        public IActionResult Addadmin()
        {
            return View();
        }  
        
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task< IActionResult >Addadmin(RegisterUserViewModel newUserVM, IFormFile imageurl)
		{
			// Ensure server-side validation is met
			if (newUserVM.Name != null)
			{
				// Create a new ApplicationUser object
				ApplicationUser userModel = new ApplicationUser
				{
					Name = newUserVM.Name,
					UserName = newUserVM.Username, // Create a unique username
					Email = newUserVM.Email,
					Gender = newUserVM.Gender,
					PhoneNumber = newUserVM.Phone,  // Add Phone number as per the model
				};

				// Handle image upload
				if (imageurl != null && imageurl.Length > 0)
				{
					// Ensure a unique file name to avoid overwriting
					var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(imageurl.FileName);

					// Determine the path to save the image in wwwroot/images folder
					var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", uniqueFileName);
					using (var stream = new FileStream(imagePath, FileMode.Create))
					{
						await imageurl.CopyToAsync(stream);
					}

					// Store the image path in the ApplicationUser object
					userModel.imageurl = "/images/" + uniqueFileName;  // Relative URL to the image
				}

				// Create the user with the UserManager and set the password
				IdentityResult result = await userManager.CreateAsync(userModel, newUserVM.Password);

				// Check if the user creation was successful
				if (result.Succeeded)
				{
                    await userManager.AddToRoleAsync(userModel, "admin");
					//create Cookie
					await signInManager.SignInAsync(userModel, isPersistent: false);
					// Redirect to Home after successful registration
					return RedirectToAction("Index", "Home");
				}
				else
				{
					// Add errors to ModelState to display on the form
					foreach (var error in result.Errors)
					{
						ModelState.AddModelError(string.Empty, error.Description);
					}
				}
			}

			// If ModelState is invalid, redisplay the form with validation errors
			return View(newUserVM);
		}


    }
}
