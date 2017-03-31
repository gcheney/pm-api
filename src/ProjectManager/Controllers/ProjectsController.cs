using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProjectManager.Services;
using ProjectManager.Models;
using ProjectManager.Entities;
using AutoMapper;

namespace ProjectManager.Controllers
{
    [Route("api/projects")]
    public class ProjectsController : Controller
    {
        private readonly IProjectManagerRepository _projectManagerRepository;
        private readonly ILogger<ProjectsController> _logger;

        public ProjectsController(ILoggerFactory loggerFactory,
            IProjectManagerRepository projectManagerRepository)
        {
            _projectManagerRepository = projectManagerRepository;
            _logger = loggerFactory.CreateLogger<ProjectsController>();
        }

        // GET api/projects
        [HttpGet()]
        public async Task<IActionResult> GetProjects()
        {
            var projectEntities = await _projectManagerRepository.GetAllProjectsAsync();
            var projectDtos = Mapper.Map<IEnumerable<ProjectDto>>(projectEntities);
            return Ok(projectDtos);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetProject(int id, bool includeUserStories = false)
        {
            var project = await _projectManagerRepository.GetProjectByIdAsync(id, includeUserStories);

            if (project == null)
            {
                _logger.LogInformation($"No project found with id {id}");
                return NotFound();
            }

            if (includeUserStories)
            {
                var projectWithUserStory = Mapper.Map<ProjectDto>(project);
                return Ok(projectWithUserStory);
            }
            else 
            {
                var projectWithoutUserStory = Mapper.Map<ProjectWithoutUserStoriesDto>(project);
                return Ok(projectWithoutUserStory);
            }
        }

        [HttpPost()]
        public async Task<IActionResult> CreateProject([FromBody] CreateProjectDto project)
        {
            if (project == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var projectToCreate = Mapper.Map<Project>(project);

            _projectManagerRepository.AddProjectAsync(projectToCreate);

            if (! await _projectManagerRepository.SaveAsync())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            var createdProjectToReturn = Mapper.Map<ProjectDto>(projectToCreate);

            return CreatedAtRoute("GetProject", new 
                { 
                    id = createdProjectToReturn.Id, 
                }, 
                createdProjectToReturn);
        }  

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var project = await _projectManagerRepository.GetProjectByIdAsync(id, true);

            if (project == null)
            {
                return NotFound();
            }

            _projectManagerRepository.DeleteProject(project);

            if (!await _projectManagerRepository.SaveAsync())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            return NoContent();
        }
        
    }
}
