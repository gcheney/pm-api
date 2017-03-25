using System.Threading.Tasks;
using System.Collections.Generic;
using ProjectManager.Entities;

namespace ProjectManager.Services
{
    public interface IProjectManagerRepository
    {
        Task<IEnumerable<Project>> GetAllProjectsAsync();

        Task<Project> GetProjectByIdAsync(int id, bool includeUserStories);

        Task<IEnumerable<UserStory>> GetUserStoriesByProjectIdAsync(int projectId);

        Task<UserStory> GetUserStoryByIdAsync(int projectId, int userStoryId);

        Task<bool> ProjectExistAsync(int projectId);

        void AddUserStoryForProjectAsync(int projectId, UserStory userStoryToSave);

        void DeleteUserStory(UserStory userStory);

        Task<bool> SaveAsync();
    }
}