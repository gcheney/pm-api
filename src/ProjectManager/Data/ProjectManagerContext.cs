using Microsoft.EntityFrameworkCore;
using ProjectManager.Entities;

namespace ProjectManager.Data
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
            optionsBuilder.UseSqlite("Filename=./ProjectManager.db");
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<UserStory> UserStories { get; set; }
    }
}