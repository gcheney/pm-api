using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ProjectManager.Data;
using ProjectManager.Models;
using ProjectManager.Entities;
using ProjectManager.Services;
using AutoMapper;

namespace ProjectManager.Controllers
{
    [Route("api/projects")]
    public class UserStoriesController : Controller
    {
        private ILogger<UserStoriesController> _logger;
        private IProjectManagerRepository _projectManagerRepository;

        public UserStoriesController(ILoggerFactory loggerFactory,
            IProjectManagerRepository projectManagerRepository)
        {
            _projectManagerRepository = projectManagerRepository;
            _logger = loggerFactory.CreateLogger<UserStoriesController>();
        }

        // GET: api/projects/22/userstories
        [HttpGet("{id:int}/userstories")]
        public async Task<IActionResult> GetProjectUserStories(int id)
        {
            var userStories = await _projectManagerRepository.GetUserStoriesByProjectIdAsync(id);

            if (userStories == null)
            {
                _logger.LogInformation($"Could not find user stories for project with id: {id}");
                return NotFound();
            }

            var userStoriesDto = Mapper.Map<IEnumerable<UserStoryDto>>(userStories);
            return Ok(userStoriesDto);
        }

        // GET: api/projects/22/userstories/5
        [HttpGet("{projectId:int}/userstories/{id:int}", Name = "GetUserStory")]
        public async Task<IActionResult> GetUserStory(int projectId, int id)
        {
            var userStory = await _projectManagerRepository.GetUserStoryByIdAsync(projectId, id);

            if (userStory == null)
            {
                _logger.LogInformation($"Could not find user story with id: {id}");
                return NotFound();
            }

            var userStoryDto = Mapper.Map<UserStoryDto>(userStory);
            return Ok(userStoryDto);
        }

        [HttpPost("{projectId:int}/userstories")]
        public async Task<IActionResult> CreateUserStory(int projectId, 
            [FromBody] CreateUserStoryDto userStory)
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

            if (! await _projectManagerRepository.ProjectExistAsync(projectId))
            {
                return NotFound();
            }

            var userStoryToSave = Mapper.Map<UserStory>(userStory);

            _projectManagerRepository.AddUserStoryForProjectAsync(projectId, userStoryToSave);

            if (! await _projectManagerRepository.SaveAsync())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            var createdUserStoryToReturn = Mapper.Map<UserStoryDto>(userStoryToSave);

            return CreatedAtRoute("GetUserStory", new 
                { 
                    projectId = projectId, 
                    id = createdUserStoryToReturn.Id 
                }, 
                createdUserStoryToReturn);
        }   

        [HttpPut("{projectId:int}/userstories/{id:int}")]
        public async Task<IActionResult> UpdateUserStory(int projectId, int id,     
            [FromBody] UpdateUserStoryDto userStory)
        {
            if (userStory == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (! await _projectManagerRepository.ProjectExistAsync(projectId))
            {
                return NotFound();
            }

            var userStoryEntity = await _projectManagerRepository.GetUserStoryByIdAsync(projectId, id);
            if (userStoryEntity == null)
            {
                return NotFound();
            }

            Mapper.Map(userStory, userStoryEntity);

            if (!await _projectManagerRepository.SaveAsync())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            return NoContent();
        }

        [HttpPatch("{projectId:int}/userstories/{id:int}")]
        public async Task<IActionResult> PartiallyUpdateUserStory(int projectId, int id,
            [FromBody] JsonPatchDocument<UpdateUserStoryDto> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            if (!await _projectManagerRepository.ProjectExistAsync(projectId))
            {
                return NotFound();
            }

            var userStoryEntity = await _projectManagerRepository.GetUserStoryByIdAsync(projectId, id);
            if (userStoryEntity == null)
            {
                return NotFound();
            }

            var userStoryToPatch = Mapper.Map<UpdateUserStoryDto>(userStoryEntity);

            patchDoc.ApplyTo(userStoryToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (userStoryToPatch.Description == userStoryToPatch.Name)
            {
                ModelState.AddModelError("Description", "The provided description should be different from the name.");
            }

            TryValidateModel(userStoryToPatch);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Mapper.Map(userStoryToPatch, userStoryEntity);

            if (!await _projectManagerRepository.SaveAsync())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            return NoContent();
        }        

        [HttpDelete("{projectId:int}/userstories/{id:int}")]
        public async Task<IActionResult> DeletePointOfInterest(int projectId, int id)
        {
            if (!await _projectManagerRepository.ProjectExistAsync(projectId))
            {
                return NotFound();
            }

            var userStoryEntity = await _projectManagerRepository.GetUserStoryByIdAsync(projectId, id);

            if (userStoryEntity == null)
            {
                return NotFound();
            }

            _projectManagerRepository.DeleteUserStory(userStoryEntity);

            if (!await _projectManagerRepository.SaveAsync())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            return NoContent();
        }
    }
}
