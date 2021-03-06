using System;
using System.Linq;
using System.Collections.Generic;
using ProjectManager.Data;
using ProjectManager.Entities;

namespace ProjectManager.Extensions
{
    public static class PorjectManagerExtensions
    {
        public static void EnsureSeedDataForContext(this ProjectManagerContext context)
        {
            if (context.Projects.Any())
            {
                return; // database already seeded
            }

            var projects = new List<Project>()
            {
                new Project()
                {
                    Id = 1,
                    Name = "Project 1",
                    Description = "The first project",
                    UserStories = new List<UserStory>()
                    {
                        new UserStory()
                        {
                            Id = 1, 
                            Name = "Add Login Screen",
                            Description = "A login screen needs to be added to the homepage",
                            WorkRemaining = "3 hours",
                            Completed = false
                        }
                    }
                },
                new Project()
                {
                    Id = 2,
                    Name = "Project 2",
                    Description = "The second project",
                },
                new Project()
                {
                    Id = 3,
                    Name = "Project Alpha",
                    Description = "The finished project",
                }
            };

            context.Projects.AddRange(projects);
            if (context.SaveChanges() > 0)
            {
                Console.WriteLine("Data seeded to ProjectManagerContext");
            }
        }
    }
}