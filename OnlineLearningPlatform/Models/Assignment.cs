namespace OnlineLearningPlatform.Models
{
    public class Assignment
    {
        public int Id { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;

        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;

        public DateTime DueDate { get; set; }

        public int MaxScore { get; set; }
        public ICollection<Submission> Submissions { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
