namespace ProjectManager.Models
{
    public class DeveloperDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Level { get; set; }

        public string FullName 
        { 
            get
            {
                return $"{this.FirstName} {this.LastName}";
            }
        }
    }
}