using Microsoft.AspNetCore.Mvc;

namespace BanSachCu.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    public class HomeController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Profile()
        {
            return View();
        }
    }
}
