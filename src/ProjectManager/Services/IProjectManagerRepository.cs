using System.Threading.Tasks;
using System.Collections.Generic;
using ProjectManager.Entities;

namespace ProjectManager.Services
{
    public interface IProjectManagerRepository
    {
        Task<IEnumerable<Project>> GetAllProjectsAsync();

        Task<Project> GetProjectByIdAsync(int id, bool includeUserStories);

        Task<IEnumerable<UserStory>> GetUserStoriesForProjectAsync(int projectId);

        Task<UserStory> GetUserStoryByIdAsync(int projectId, int userStoryId);
    }
}