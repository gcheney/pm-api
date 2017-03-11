using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
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

        [HttpPost("{cityId}/userstories")]
        public IActionResult CreateUserStory(int projectId, [FromBody] CreateUserStoryDto userStory)
        {
            if (userStory == null)
            {
                return BadRequest();
            }

            // how to add model state error 
            if (userStory.Description == userStory.Name)
            {
                ModelState.AddModelError("Details", "The provided description should be different than the Name");
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

            // for  InMemoryDataStore id increment
            var maxUserStoryId = InMemoryDataStore.Current.Projects.SelectMany(
                    p => p.UserStories).Max(us => us.Id);

            var newUserStory = new UserStoryDto()
            {
                Id = ++maxUserStoryId,
                Name = userStory.Name,
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

        [HttpPut("{projectId}/userstories/{id}")]
        public IActionResult UpdateUserStory(int projectId, int id, [FromBody] UpdateUserStoryDto userStory)
        {
            if (userStory == null)
            {
                return BadRequest();
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

            var userStoryToUpdate = project.UserStories.FirstOrDefault(us => us.Id == id);

            if (userStoryToUpdate == null)
            {
                return NotFound();
            }

            userStoryToUpdate.Name = userStory.Name;
            userStoryToUpdate.Description = userStory.Description;
            userStoryToUpdate.WorkRemaining = userStory.WorkRemaining;
            userStoryToUpdate.Completed = userStory.Completed;


            return NoContent();
        }

        [HttpPatch("{projectId}/userstories/{id}")]
        public IActionResult PartiallyUpdateUserStory(int projectId, int id,
            [FromBody] JsonPatchDocument<UpdateUserStoryDto> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            var project = InMemoryDataStore.Current.Projects.FirstOrDefault(p => p.Id == projectId);
            if (project == null)
            {
                return NotFound();
            }

            var userStory = project.UserStories.FirstOrDefault(us => us.Id == id);
            if (userStory == null)
            {
                return NotFound();
            }

            var userStoryToPatch = new UpdateUserStoryDto()
            {
                Name = userStory.Name,
                Description = userStory.Description,
                WorkRemaining = userStory.WorkRemaining,
                Completed = userStory.Completed
            };

            patchDoc.ApplyTo(userStoryToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            TryValidateModel(userStoryToPatch);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            userStory.Name = userStoryToPatch.Name;
            userStory.Description = userStoryToPatch.Description;

            return NoContent();
        }        

        [HttpDelete("{projectId}/userstories/{id}")]
        public IActionResult DeletePointOfInterest(int projectId, int id)
        {
            var project = InMemoryDataStore.Current.Projects.FirstOrDefault(p => p.Id == projectId);
            if (project == null)
            {
                return NotFound();
            }

            var userStoryToDelete = project.UserStories.FirstOrDefault(us => us.Id == id);
            if (userStoryToDelete == null)
            {
                return NotFound();
            }

            project.UserStories.Remove(userStoryToDelete);

            return NoContent();
        }
    }
}
