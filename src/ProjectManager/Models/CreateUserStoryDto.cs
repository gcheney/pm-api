namespace ProjectManager.Models
{
    public class CreateUserStoryDto
    {
        public string Title { get; set; }
        public string Details { get; set; }
        public string WorkRemaining { get; set; }
        public bool Completed { get; set; }
    }
}