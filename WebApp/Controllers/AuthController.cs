using Data.Context;
using Data.Entities;
using Microsoft.AspNetCore.Identity;
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

            if (_context.Users.Any(x => x.Email == model.Email)) 
            {
                ModelState.AddModelError("Email", "There is already a account with that Email");
                ViewBag.View = "Register";
                return View("Index", model);
            }

            var user = new UserEntity
            {
                FullName = model.FullName,
                Email = model.Email,
            };

            var hasher = new PasswordHasher<UserEntity>();
            user.Password = hasher.HashPassword(user, model.Password);

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

            var user = _context.Users.FirstOrDefault(x => x.Email == model.Email);

            if (user != null)
            {
                var hasher = new PasswordHasher<UserEntity>();
                var result = hasher.VerifyHashedPassword(user, user.Password, model.Password);

                if (result == PasswordVerificationResult.Success) 
                {
                    HttpContext.Session.SetString("Email", user.Email);
                    return RedirectToAction("Index", "Home");
                }

            }

            ModelState.AddModelError(string.Empty, "Wrong Email or Password!");
            ViewBag.View = "Login";
            return View("Index", model);
            
              
        }
    }
}
