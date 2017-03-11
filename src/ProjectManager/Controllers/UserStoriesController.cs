using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjectManager.Data;
using ProjectManager.Models;

namespace ProjectManager.Controllers
{
    [Route("api/projects")]
    public class UserStoriesController : Controller
    {
        // GET: api/projects/22/userstories
        [HttpGet("{id:int}/userstories")]
        public IActionResult GetProjectUserStories(int id)
        {
            var project = InMemoryDataStore.Current.Projects.FirstOrDefault(p => p.Id == id);

            if (project == null)
            {
                return NotFound();
            }

            return Ok(project.UserStories);
        }

        // GET: api/projects/22/userstories/5
        [HttpGet("{projectId:int}/userstories/{id:int}", Name = "GetUserStory")]
        public IActionResult GetUserStory(int projectId, int id)
        {
            var project = InMemoryDataStore.Current.Projects.FirstOrDefault(p => p.Id == projectId);

            if (project == null)
            {
                return NotFound();
            }

            var UserStory = project.UserStories.FirstOrDefault(d => d.Id == id);

            if (UserStory == null)
            {
                return NotFound();
            }

            return Ok(UserStory);
        }

        [HttpPostAttribute("{cityId}/userstories")]
        public IActionResult CreateUserStory(int projectId, [FromBody] CreateUserStoryDto userStory)
        {
            if (userStory == null)
            {
                return BadRequest();
            }

            // add model state error
            if (userStory.Description == userStory.Title)
            {
                ModelState.AddModelError("Details", "The provided description should be different than the title");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var project = InMemoryDataStore.Current.Projects.FirstOrDefault(p => p.Id == projectId);

            if (project == null)
            {
                return NotFound();
            }

            // for  InMemoryDataStore
            var maxUserStoryId = InMemoryDataStore.Current.Projects.SelectMany(
                    p => p.UserStories).Max(us => us.Id);

            var newUserStory = new UserStoryDto()
            {
                Id = ++maxUserStoryId,
                Title = userStory.Title,
                Description = userStory.Description,
                WorkRemaining = userStory.WorkRemaining,
                Completed = userStory.Completed
            };

            project.UserStories.Add(newUserStory);

            return CreatedAtRoute("GetUserStory", new {
                projectId = projectId,
                id = newUserStory.Id
            });
        }   
    }
}
