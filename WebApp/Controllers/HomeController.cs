using Data.Context;
using Data.Entities;
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

            var projects = new List<ProjectEntity>();

            var teamMembers = _context.Users.Where(x => x.Id != user.Id).ToList();

            var model = new HomeViewModel
            {
                Profile = profile,
                TeamMembers = teamMembers
            };

            ViewData["Section"] = section;


            return View(model);
        }

        [HttpPost]
        public IActionResult CreateProject(CreateProjectViewModel model)
        {
            if (!ModelState.IsValid)
            {
                foreach (var key in ModelState.Keys)
                {
                    var state = ModelState[key];
                    if (state?.Errors.Count > 0)
                    {
                        Console.WriteLine($"{key}: {state.Errors[0].ErrorMessage}");
                    }
                }

                return BadRequest("Invalid data.");
            }

            var user = _context.Users.FirstOrDefault(x => x.Email == User.Identity.Name);
            if (user == null)
                return BadRequest("Not logged in");

            var project = new ProjectEntity
            {
                Image = "/images/ProjectImage1.svg",
                ProjectName = model.ProjectName,
                ClientName = model.ClientName,
                Description = model.Description,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                Budget = model.Budget,
                UserId = user.Id,
            };

            _context.Projects.Add(project);
            _context.SaveChanges();

            return Ok();
        }
    }
}
