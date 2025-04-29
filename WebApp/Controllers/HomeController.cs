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

        [HttpGet]
        public IActionResult Index(string section = "Projects")
        {
            var email = User.Identity?.Name;
            var user = _context.Users.FirstOrDefault(x => x.Email == email);

            if (user == null)
                return RedirectToAction("Login", "Auth");

            var profile = new UserEditViewModel
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                CurrentAvatarUrl = user.AvatarUrl
            };

            var projects = _context.Projects.Where(x => x.Id == profile.Id).ToList();

            var model = new HomeViewModel
            {
                Profile = profile,
                Projects = projects
            };

            ViewData["Section"] = section;


            return View(model);
        }
    }
}
