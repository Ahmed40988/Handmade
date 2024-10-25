using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.Features;
using Handmade.Models;
using Microsoft.AspNetCore.Identity;



namespace Handmade
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // إضافة سلسلة الاتصال الخاصة بك
            builder.Services.AddDbContext<DataDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // إعداد الهوية (Identity) وإضافة سياق البيانات
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(Options=>
            {
                Options.Password.RequireUppercase=false;
                Options.Password.RequireLowercase=false;
                Options.Password.RequireDigit=false;
                Options.Password.RequireNonAlphanumeric = false;
                Options.Password.RequiredLength=4;


            })
                .AddEntityFrameworkStores<DataDbContext>();

            // تكوين خيارات الهوية (IdentityOptions)
            builder.Services.Configure<IdentityOptions>(options =>
            {
                options.User.AllowedUserNameCharacters =
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                options.User.RequireUniqueEmail = true; // إذا كان البريد الإلكتروني يجب أن يكون فريدًا
            });

            // إعدادات الحد الأقصى لحجم الملفات
            builder.Services.Configure<FormOptions>(options =>
            {
                options.MultipartBodyLengthLimit = 10 * 1024 * 1024; // 10 ميغابايت
            });

            // إضافة خدمات الجلسة
            builder.Services.AddDistributedMemoryCache(); // يستخدم لتخزين بيانات الجلسات في الذاكرة
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30); // مدة الجلسة 30 دقيقة
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            // إضافة الخدمات إلى الحاوية.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // تكوين قناة طلب HTTP.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            
            app.UseSession();

            
            app.UseAuthentication();  // Filter Authorize cookie 
            app.UseAuthorization(); // Determines your role and what parts of the site you can access


            // تفعيل الجلسات في التطبيق
            app.UseSession();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
