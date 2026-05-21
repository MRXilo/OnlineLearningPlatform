namespace OnlineLearningPlatform.Models
{
    public class Lesson
    {
        public int Id { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;

        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;

        public int OrderIndex { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
