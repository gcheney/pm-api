using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectManager.Entities;
using ProjectManager.Data;

namespace ProjectManager.Services
{
    public class ProjectManagerRepository : IProjectManagerRepository
    {
        private ProjectManagerContext _context;

        public ProjectManagerRepository(ProjectManagerContext content)
        {
            _context = content;
        }

        public async Task<IEnumerable<Project>> GetAllProjectsAsync()
        {
            return await _context.Projects.OrderBy(p => p.Name)
                .ToListAsync();
        }

        public async Task<Project> GetProjectByIdAsync(int id, bool includeUserStories)
        {
            if (includeUserStories)
            {
                return await _context.Projects
                    .Where(p => p.Id == id)
                    .Include(p => p.UserStories)
                    .FirstOrDefaultAsync();
            }

            return await _context.Projects
                .Where(p => p.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<UserStory>> GetUserStoriesByProjectIdAsync(int projectId)
        {
            return await _context.UserStories
                .Where(us => us.ProjectId == projectId)
                .OrderBy(us => us.Name)
                .ToListAsync();
        }

        public async Task<UserStory> GetUserStoryByIdAsync(int projectId, int userStoryId)
        {
            return await _context.UserStories
                .Where(us => us.ProjectId == projectId && us.Id == userStoryId)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> ProjectExistAsync(int projectId)
        {
            return await _context.Projects.AnyAsync(p => p.Id == projectId);
        }

        public async void AddUserStoryForProjectAsync(int projectId, UserStory userStoryToSave)
        {
            var project = await GetProjectByIdAsync(projectId, false);
            project.UserStories.Add(userStoryToSave);
        }

        public async Task<bool> SaveAsync()
        {
            return (await _context.SaveChangesAsync() > 0);
        }
    }
}