namespace OnlineLearningPlatform.Models
{
    public class Submission
    {
        public int Id { get; set; }

        public int AssignmentId { get; set; }
        public Assignment Assignment { get; set; } = null!;

        public int StudentId { get; set; }
        public User Student { get; set; } = null!;

        public string Content { get; set; } = null!;

        public int? Score { get; set; }
        public string? Feedback { get; set; }
        public double? Grade { get; set; }
        
        public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;
    }
}
