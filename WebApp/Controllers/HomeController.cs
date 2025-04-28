using Data.Context;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataContext _context;

        public HomeController(DataContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var email = User.Identity?.Name;
            var user = _context.Users.FirstOrDefault(x => x.Email == email);

            if (user == null)
                return RedirectToAction("Login", "Auth");

            var model = new UserEditViewModel
            {
                FullName = user.FullName,
                Email = user.Email,
                CurrentAvatarUrl = user.AvatarUrl
            };

            return View(model);
        }
    }
}
