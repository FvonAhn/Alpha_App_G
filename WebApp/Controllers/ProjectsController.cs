using Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using Data.Entities;
using System.Security.Claims;

namespace WebApp.Controllers
{
    [Authorize]
    public class ProjectsController : Controller
    {
        private readonly ProjectRepository _projectRepository;

        public ProjectsController(ProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var projects = await _projectRepository.GetAllProjectsAsync();
            return View(projects);
        }

        [HttpGet]
        public IActionResult CreateProject()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProject(ProjectViewModel model)
        {
            if (!ModelState.IsValid) 
            {
                TempData["Error"] = "Wrong information";
                return View(model);
            }

            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var project = new ProjectEntity
            {
                ProjectName = model.ProjectName,
                ClientName = model.ClientName,
                Description = model.Description,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                Budget = model.Budget,
                UserId = userId
            };

            await _projectRepository.CreateProjectAsync(project);

            TempData["Success"] = "Project created.";
            return RedirectToAction("Index", "Home");

        }
    }
}

