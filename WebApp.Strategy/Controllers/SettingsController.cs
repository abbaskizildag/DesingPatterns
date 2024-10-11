using BaseProject.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApp.Strategy.Models;

namespace WebApp.Strategy.Controllers
{
    [Authorize]
    public class SettingsController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public SettingsController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            //cookie'de claims inde data var mı
            Settings settings = new();
            if (User.Claims.Where(x=>x.Type==Settings.claimDatabaseType).FirstOrDefault() != null)
            {
                settings.DatabaseType =(EDatabaseType) int.Parse(User.Claims.First(x=>x.Type == Settings.claimDatabaseType).Value);
            }
            else
            {
                settings.DatabaseType = settings.GetDefaultDbType;
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangeDatabase(int databaseType)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var newClaim = new Claim(Settings.claimDatabaseType, databaseType.ToString());

            var claims = await _userManager.GetClaimsAsync(user);

            var hasDatabaseTypeClaim = claims.FirstOrDefault(x => x.Type == Settings.claimDatabaseType);

            if (hasDatabaseTypeClaim is not null)
            {
                await _userManager.ReplaceClaimAsync(user, hasDatabaseTypeClaim, newClaim);
            }
            else
            {
                await _userManager.AddClaimAsync(user, newClaim);
            }

            await _signInManager.SignOutAsync(); //çıkış yaptırıyoruz. çünkü Cookie'de eskisi kalmış olabilir.
            var authenticareResult = await HttpContext.AuthenticateAsync();

            //framework tekrar yeni Cookie oluştursun diye tekrar login işlemi yapıyoruz.   
            await _signInManager.SignInAsync(user, authenticareResult.Properties);

            return RedirectToAction(nameof(Index));
        }
    }   
}
