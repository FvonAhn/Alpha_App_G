﻿using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class TeamMemberController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
