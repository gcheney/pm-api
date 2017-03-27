using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProjectManager.Services;
using ProjectManager.Models;
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
