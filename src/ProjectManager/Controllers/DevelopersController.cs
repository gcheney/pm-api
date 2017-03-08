using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjectManager.Data;

namespace ProjectManager.Controllers
{
    [Route("api/projects")]
    public class DevelopersController : Controller
    {
        // GET: api/projects/22/developers
        [HttpGet("{id:int}/developers")]
        public IActionResult GetProjectDevelopers(int id)
        {
            var project = InMemoryDataStore.Current.Projects
                .FirstOrDefault(p => p.Id == id);

            if (project == null)
            {
                return NotFound();
            }

            return Ok(project.Developers);
        }

        // GET: api/projects/22/developers/5
        [HttpGet("{projectId:int}/developers/{id:int}")]
        public IActionResult GetSingleDeveloper(int projectId, int id)
        {
            var project = InMemoryDataStore.Current.Projects
                .FirstOrDefault(p => p.Id == projectId);

            if (project == null)
            {
                return NotFound();
            }

            var developer = project.Developers.FirstOrDefault(d => d.Id == id);

            if (developer == null)
            {
                return NotFound();
            }

            return Ok(developer);
        }
    }
}
