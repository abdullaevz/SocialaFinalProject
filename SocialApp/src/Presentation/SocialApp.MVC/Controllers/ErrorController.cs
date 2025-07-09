using Microsoft.AspNetCore.Mvc;

namespace SocialApp.MVC.Controllers;

public class ErrorController : Controller
{
    //[Route("Error")]
    public IActionResult Error()
    {
        return View();
    }
}
