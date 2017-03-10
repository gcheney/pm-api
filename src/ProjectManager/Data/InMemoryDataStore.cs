using System;
using System.Collections.Generic;
using ProjectManager.Models;

namespace ProjectManager.Data
{
    public class InMemoryDataStore
    {
        public static InMemoryDataStore Current { get; } = new InMemoryDataStore();
        public List<ProjectDto> Projects { get; set; }

        public InMemoryDataStore()
        {
            Projects = new List<ProjectDto>()
            {
                new ProjectDto()
                {
                    Id = 1,
                    Name = "Project 1",
                    Description = "The first project",
                    ReleaseDate = new DateTime(2018, 2, 1),
                    UserStories = new List<UserStoryDto>()
                    {
                        new UserStoryDto()
                        {
                            Id = 1, 
                            Title = "Add Login Screen",
                            Details = "A login screen needs to be added to the homepage",
                            WorkRemaining = "3 hours",
                            Completed = false
                        }
                    }
                },
                new ProjectDto()
                {
                    Id = 2,
                    Name = "Project 2",
                    Description = "The second project",
                    ReleaseDate = new DateTime(2019, 1, 1)
                },
                new ProjectDto()
                {
                    Id = 3,
                    Name = "Project Alpha",
                    Description = "The finished project",
                    ReleaseDate = new DateTime(2016, 1, 1),
                }
            };
        }
    }
}