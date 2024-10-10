using BaseProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BaseProject.Controllers
{
    public class AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager) : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email,  string password)
        {
            var hasUser =  await userManager.FindByEmailAsync(email);
            if (hasUser is null)
            {
                return View();
            }

            var signInResult = signInManager.PasswordSignInAsync(hasUser, password,true,false);

            if (!signInResult.Result.Succeeded)
            {
                return View();
            }

            return RedirectToAction(nameof(HomeController.Index),"Home");
        }

        public async Task<IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}
