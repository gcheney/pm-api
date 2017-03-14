using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ProjectManager.Entities
{
    public class ProjectManagerContext : DbContext
    {
        public ProjectManagerContext(DbContextOptions<ProjectManagerContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=./Thoughtwave.db");
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<UserStory> UserStories { get; set; }
    }
}