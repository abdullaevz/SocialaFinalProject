﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SocialApp.MVC.Areas.Admin.Controllers;



[Area("Admin")]
[Authorize(Roles = "Admin")]
public class DashboardController : Controller
{

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
}
