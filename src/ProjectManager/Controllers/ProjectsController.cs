using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProjectManager.Services;
using ProjectManager.Data;
using ProjectManager.Entities;
using ProjectManager.Models;
using AutoMapper;

namespace ProjectManager.Controllers
{
    [Route("api/projects")]
    public class ProjectsController : Controller
    {
        private IProjectManagerRepository _projectManagerRepository;
        private ILogger<UserStoriesController> _logger;

        public ProjectsController(IProjectManagerRepository projectManagerRepository,
            ILogger<UserStoriesController> logger)
        {
            _projectManagerRepository = projectManagerRepository;
            _logger = logger;
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
        public async Task<IActionResult> GetProject(int id, bool includeUserStories)
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
        
    }
}
