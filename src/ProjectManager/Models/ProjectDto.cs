using System;
using System.Collections.Generic;

namespace ProjectManager.Models
{
    public class ProjectDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public bool Completed { get; set; }

        public ICollection<DeveloperDto> Developers { get; set; } 
            = new List<DeveloperDto>();
    }
}