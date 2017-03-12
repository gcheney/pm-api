using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManager.Entities
{
    public class UserStory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string WorkRemaining { get; set; }
        public bool Completed { get; set; }

        public int ProjectId { get; set; }
        public Project Project { get; set; }
    }
}