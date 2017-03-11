using System.ComponentModel.DataAnnotations;

namespace ProjectManager.Models
{
    public class CreateUserStoryDto
    {
        [Required(ErrorMessage = "A User Story must include a title")]
        [MaxLength(50, ErrorMessage = "Title cannot exceed 50 characters")]
        public string Title { get; set; }

        [MaxLength(200, ErrorMessage = "Description cannot exceed 200 characters")]
        public string Description { get; set; }

        [MaxLength(25, ErrorMessage = "Work remaining cannot exceed 200 characters")]
        public string WorkRemaining { get; set; }

        [Required(ErrorMessage = "Completed status must be included")]
        public bool Completed { get; set; }
    }
}