namespace ProjectManager.Models
{
    public class DeveloperDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }

        public string FullDetails 
        { 
            get
            {
                return $"{this.FirstName} {this.LastName} - {this.Title}";
            }
        }
    }
}