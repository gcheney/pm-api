using Microsoft.EntityFrameworkCore;

namespace ProjectManager.Entities
{
    public class ProjectManagerContext : DbContext
    {
        public ProjectManagerContext(DbContextOptions<ProjectManagerContext> options)
            : base(options)
        {
            Database.Migrate();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=./ProjectManager.db");
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<UserStory> UserStories { get; set; }
    }
}