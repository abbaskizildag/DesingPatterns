using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Strategy.Models;

namespace WebApp.Strategy.Controllers
{
    [Authorize]
    public class SettingsController : Controller
    {
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
    }   
}
