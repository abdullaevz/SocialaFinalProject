using Microsoft.AspNetCore.Mvc;

namespace SocialApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
