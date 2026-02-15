using Firm.Infrastructure.Auth;
using Firm.Infrastructure.Data;
using FirmWebApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FirmWebApp.Controllers.UserLogIn
{
    public class User_LoginController : Controller
    {
        private readonly FirmDBContext db;
        private UserManager<ApplicationUser> userManager;
        private RoleManager<IdentityRole> roleManager;
        private SignInManager<ApplicationUser> signInManager;
        private IWorkContext workContext;

        public User_LoginController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
            IWorkContext workContext, RoleManager<IdentityRole> roleManager, FirmDBContext db)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.workContext = workContext;
            this.roleManager = roleManager;
            this.db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            LogInViewModel model = new LogInViewModel();
            return View(model);
        }
        public IActionResult AccessDenied()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LogInViewModel login)
        {


            var user = db.Users.FirstOrDefault(f => f.UserName == login.UserName && f.IsActive == true);
            if (user != null)
            {
                //var passwordHash = userManager.PasswordHasher.HashPassword(user,"123456");

                var result = await signInManager.PasswordSignInAsync(user, login.Password, false, false);
                if (result.Succeeded)
                {
                    if (login.ReturnUrl == null)
                    {
                        if (user.UserType == 1)
                        {

                            return RedirectToAction("Index", "Admin");
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }

                    }
                    else
                    {
                        return Redirect(login.ReturnUrl);
                    }
                   
                }

            }
            ModelState.AddModelError(string.Empty, "User not found!");
            return View(login);
        }

        [HttpGet]

        public async Task<IActionResult> Logout()
        {
            if (signInManager.IsSignedIn(User))
            {
                await signInManager.SignOutAsync();
            }
            return RedirectToAction("Login");
        }
        private bool CheckValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
