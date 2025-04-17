using Data.Context;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class AuthController : Controller
    {
        private readonly DataContext _context;

        public AuthController(DataContext context) 
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index(string view = "Login")
        {
            ViewBag.View = view;
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.View = "Register";
                return View("Index", model);
            }

            if (_context.Users.Any(u => u.Email == model.Email)) 
            {
                ModelState.AddModelError("Email", "There is already a account with that Email");
                ViewBag.View = "Register";
                return View("Index", model);
            }

            var user = new UserEntity
            {
                FullName = model.FullName,
                Email = model.Email,
                Password = model.Password,
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            TempData["Success"] = "Account created. You may now log in.";
            return RedirectToAction("Index", new { view = "Login" });
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.View = "Login";
                return View("Index", model);
            }
                

            return RedirectToAction("Index", "Home");
        }
    }
}
