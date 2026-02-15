using Firm.Infrastructure.Auth;
using FirmWebApp.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FirmWebApp.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    public class UserManagementController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserManagementController(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }


        [HttpGet]
        public IActionResult CreateUser()
        {
            ViewBag.Roles = new SelectList(_roleManager.Roles.Select(r => r.Name).ToList());
            return View(new ViewModel.CreateUserViewModel());
        }



        //Main
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUser(CreateUserViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Password))
            {
                ModelState.AddModelError("Password", "Password is required");
            }

            if (!ModelState.IsValid)
            {
                // 🔐 SECURITY FIX
                model.Password = null;
                ModelState.Remove(nameof(model.Password));

                ViewBag.Roles = new SelectList(
                    _roleManager.Roles.Select(r => r.Name).ToList()
                );
                return View(model);
            }

            var user = new ApplicationUser
            {
                UserName = model.UserName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                EmailConfirmed = true,
                IsActive = true
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                // 1️⃣ Role add করুন
                var roleResult = await _userManager.AddToRoleAsync(user, model.Role);

                if (roleResult.Succeeded)
                {
                    // 2️⃣ Role অনুযায়ী UserType সেট করুন
                    user.UserType = (model.Role == "Admin") ? 1 : 0;

                    // 3️⃣ User update করুন
                    await _userManager.UpdateAsync(user);
                }

                return RedirectToAction("Index", "Admin");
            }


            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            // 🔐 Again clear password
            model.Password = null;
            ModelState.Remove(nameof(model.Password));

            ViewBag.Roles = new SelectList(_roleManager.Roles.Select(r => r.Name).ToList());
            return View(model);
        }

    }

}
