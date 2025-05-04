using Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using Data.Entities;
using System.Security.Claims;
using Data.Context;

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
            var rawId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Console.WriteLine($"[CLAIM] NameIdentifier= {rawId}");
            if (string.IsNullOrEmpty(rawId))
            {
                return Content("❌ Inloggnig sakkas, NameIdentifier finns inte");
            }
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var projects = await _projectRepository.GetAllProjectsbyUserIdAsync(userId);

            Console.WriteLine($"[DEBUG] Antal projekt för user {userId}: {projects.Count()}");

            return View(projects);
        }

        [HttpGet]
        public async Task<IActionResult> LoadProjectsPartial()
        {
            foreach (var claim in User.Claims)
            {
                Console.WriteLine($"[CLAIM] {claim.Type}: {claim.Value}");
            }

            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var projects = await _projectRepository.GetAllProjectsbyUserIdAsync(userId);
            return PartialView("Partials/_ProjectsPartial", projects);
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
                UserId = userId,
                Image = model.Image,
                IsCompleted = model.IsCompleted,
            };

            await _projectRepository.CreateProjectAsync(project);

            TempData["Success"] = "Project created.";
            return RedirectToAction("Index", "Projects");

        }

        public async Task<IActionResult> DeleteProject(int projectId)
        {
            if (projectId <= 0)
            {
                return BadRequest("No project found");
            }

           await _projectRepository.DeleteProjectAsync(projectId);

            TempData["Success"] = "Project deleted.";
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> EditProject(int projectId)
        {
            if (projectId <= 0)
            {
                return BadRequest("Invalid project");
            }

            var project = await _projectRepository.GetProjectByIdAsync(projectId);
            if (project == null)
            {
                return NotFound("Project not found");
            }

            var viewModel = new ProjectEditViewModel
            {
                Id = project.Id,
                ProjectName = project.ProjectName,
                ClientName = project.ClientName,
                Description = project.Description,
                StartDate = project.StartDate,
                EndDate = project.EndDate,
                Budget = project.Budget,
            };
            
            return PartialView("Partials/_EditProjectPartial", viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProject(ProjectEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("Partials/_EditProjectPartial", model);
            }

            var project = await _projectRepository.GetProjectByIdAsync(model.Id);
            if (project == null) 
            {
                return NotFound("Project not found");
            }

            project.ProjectName = model.ProjectName!;
            project.ClientName = model.ClientName!;
            project.Description = model.Description ?? "";
            project.StartDate = model.StartDate;
            project.EndDate = model.EndDate;
            project.Budget = model.Budget;

            await _projectRepository.UpdateProjectAsync(project);
            return Json(new { success =  true });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CompleteProject([FromBody] CompleteProjectViewModel model)
        {
            if (model == null || model.Id <= 0)
                return BadRequest();

            var project = await _projectRepository.GetProjectByIdAsync(model.Id);
            if (project == null)
                return NotFound();

            project.IsCompleted = true;
            await _projectRepository.UpdateProjectAsync(project);
            return Ok();
        }
    }
}

