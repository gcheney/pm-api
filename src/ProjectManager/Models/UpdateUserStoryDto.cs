using System.ComponentModel.DataAnnotations;

namespace ProjectManager.Models
{
    public class UpdateUserStoryDto
    {
        [Required(ErrorMessage = "A User Story must include a Name")]
        [MaxLength(50, ErrorMessage = "Name cannot exceed 50 characters")]
        public string Name { get; set; }

        [MaxLength(200, ErrorMessage = "Description cannot exceed 200 characters")]
        public string Description { get; set; }

        [MaxLength(25, ErrorMessage = "Work remaining cannot exceed 200 characters")]
        public string WorkRemaining { get; set; }

        [Required(ErrorMessage = "Completed status must be included")]
        public bool Completed { get; set; }
    }
}