using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjectManager.Data;

namespace ProjectManager.Controllers
{
    [Route("api/projects")]
    public class UserStoriesController : Controller
    {
        // GET: api/projects/22/userstories
        [HttpGet("{id:int}/userstories")]
        public IActionResult GetProjectUserStories(int id)
        {
            var project = InMemoryDataStore.Current.Projects
                .FirstOrDefault(p => p.Id == id);

            if (project == null)
            {
                return NotFound();
            }

            return Ok(project.UserStories);
        }

        // GET: api/projects/22/userstories/5
        [HttpGet("{projectId:int}/userstories/{id:int}")]
        public IActionResult GetUserStory(int projectId, int id)
        {
            var project = InMemoryDataStore.Current.Projects
                .FirstOrDefault(p => p.Id == projectId);

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
    }
}
