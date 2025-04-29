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

        public virtual async Task<IEnumerable<ProjectEntity>> GetAllProjectsAsync()
        {
            try
            {
                return await _context.Projects.ToListAsync();
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
     

                await _context.SaveChangesAsync();
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
