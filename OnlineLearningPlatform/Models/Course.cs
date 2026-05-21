namespace OnlineLearningPlatform.Models
{
    public class Course
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;

        public int TeacherId { get; set; }
        public User Teacher { get; set; } = null!;

        public string Category { get; set; } = null!;
        public int Duration { get; set; }
        public decimal Price { get; set; }
        public List<Enrollment> Enrollments { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public ICollection<Assignment> Assignments { get; set; }
    }
}
