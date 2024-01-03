namespace OnlineEducationalPlatformNEW.Models
{
    public class Courses
    {
        public int Id { get; set; }
        public string CourseId { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Description { get; set; } = string.Empty;
        public bool IsPublic { get; set; }
        public DateTime CreatedAt { get; set; }

        // New property for the owner's user ID
        public string? OwnerId { get; set; }

        public Courses()
        {
            this.CreatedAt = DateTime.Now;
        }

        public Courses(int id, string courseId, string name, string description, bool isPublic, string ownerId)
        {
            this.Id = id;
            this.CourseId = courseId;
            this.Name = name;
            this.Description = description;
            this.IsPublic = isPublic;
            this.OwnerId = ownerId;
        }
    }
}
