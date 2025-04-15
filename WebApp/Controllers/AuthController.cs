using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class AuthController : Controller
    {
        [HttpGet]
        public IActionResult Index(string view = "Login")
        {
            return View(model: view);
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
                return View("Index", "Register");

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
                return View("Index", "Login");

            return RedirectToAction("Index", "Home");
        }
    }
}
