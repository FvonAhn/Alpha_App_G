using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    using Microsoft.AspNetCore.Authorization;

    [Authorize]
    public class AccountController : Controller
    {
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Auth");
        }

        public IActionResult Edit()
        {
            return View();
        }

        public IActionResult Edit(UserEditViewModel model) 
        {
            if (ModelState.IsValid) 
            {
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

    }
}
