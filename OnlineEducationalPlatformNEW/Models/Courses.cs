namespace OnlineEducationalPlatformNEW.Models
{
    public class Courses
    {
        public int Id { get; set; }
        public string CourseId { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Description { get; set; } = string.Empty;
    }
}
