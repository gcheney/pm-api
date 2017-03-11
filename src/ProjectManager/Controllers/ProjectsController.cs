using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjectManager.Data;

namespace ProjectManager.Controllers
{
    [Route("api/projects")]
    public class ProjectsController : Controller
    {
        // GET api/projects
        [HttpGet()]
        public IActionResult GetProjects()
        {
            return Ok(InMemoryDataStore.Current.Projects);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetProject(int id)
        {
            // find project
            var project = InMemoryDataStore.Current.Projects.FirstOrDefault(p => p.Id == id);
                
            if (project == null)
            {
                return NotFound();
            }

            return Ok(project);
        }
        
    }
}
