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
                    Completed = false,
                    Developers = new List<DeveloperDto>()
                    {
                        new DeveloperDto()
                        {
                            Id = 1, 
                            FirstName = "Glen",
                            LastName = "Cheney",
                            Title = "Junior Developer"
                        },
                        new DeveloperDto()
                        {
                            Id = 2, 
                            FirstName = "Joel",
                            LastName = "Spolsky",
                            Title = "Software Architect"
                        }
                    }
                },
                new ProjectDto()
                {
                    Id = 2,
                    Name = "Project 2",
                    Description = "The second project",
                    ReleaseDate = new DateTime(2019, 1, 1),
                    Completed = false,
                    Developers = new List<DeveloperDto>()
                    {
                        new DeveloperDto()
                        {
                            Id = 3, 
                            FirstName = "Jon",
                            LastName = "Galloway",
                            Title = "Senior Developer"
                        },
                        new DeveloperDto()
                        {
                            Id = 4, 
                            FirstName = "Scott",
                            LastName = "Hanselman",
                            Title = "Senior Developer"
                        }
                    }
                },
                new ProjectDto()
                {
                    Id = 3,
                    Name = "Project Alpha",
                    Description = "The finished project",
                    ReleaseDate = new DateTime(2016, 1, 1),
                    Completed = true
                }
            };
        }
    }
}