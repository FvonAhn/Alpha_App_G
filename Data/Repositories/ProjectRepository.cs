using Data.Context;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Diagnostics;
namespace Data.Repositories
{
    public class ProjectRepository(DataContext context)
    {
        private readonly DataContext _context = context;

        // CREATE

        public virtual async Task<ProjectEntity> CreateProjectAsync(ProjectEntity project)
        {
            try
            {
                await _context.AddAsync(project);
                await _context.SaveChangesAsync();
                return project;
            }
            catch (Exception ex) 
            {
                Debug.WriteLine(ex.Message);
                return null!;
            }
        }

        // READ

        public virtual async Task<IEnumerable<ProjectEntity>> GetAllProjectsbyUserIdAsync(int userId)
        {
            try
            {
                return await _context.Projects
                    .Where(x => x.UserId == userId)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return [];
            }
        }

        public virtual async Task<ProjectEntity?> GetProjectByIdAsync(int id)
        {
            try
            {
                return await _context.Projects.FirstOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception ex) 
            {
                Debug.WriteLine(ex.Message);
                return null!;
            }
        }

        // UPDATE

        public virtual async Task<bool> UpdateProjectAsync(ProjectEntity updatedProject)
        {
            var existingProject = await _context.Projects.FirstOrDefaultAsync(x => x.Id == updatedProject.Id);
            if (existingProject == null)
            {
                return false;
            }

            try
            {
                existingProject!.ProjectName = updatedProject.ProjectName;
                existingProject.ClientName = updatedProject.ClientName;
                existingProject.Description = updatedProject.Description;
                existingProject.StartDate = updatedProject.StartDate;
                existingProject.EndDate = updatedProject.EndDate;
                existingProject.Budget = updatedProject.Budget;
                existingProject.IsCompleted = updatedProject.IsCompleted;

                _context.Entry(existingProject).Property(x => x.Description).IsModified = true;

                await _context.SaveChangesAsync();

                var saved = await _context.Projects.FirstOrDefaultAsync(x => x.Id == updatedProject.Id);
                Console.WriteLine("I databasen just nu: " + saved.Description);

                return true;
            }
            catch (Exception ex) 
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }

        // DELETE

        public virtual async Task<bool> DeleteProjectAsync(int id)
        {
            var existingProject = await _context.Projects.FirstOrDefaultAsync(x => x.Id == id);
            if (existingProject == null) 
            {
                return false; 
            }

            try
            {
                _context.Projects.Remove(existingProject!);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
