using System;
using System.Collections.Generic;

namespace ProjectManager.Models
{
    public class ProjectDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<UserStoryDto> UserStories { get; set; } = new List<UserStoryDto>();
    }
}