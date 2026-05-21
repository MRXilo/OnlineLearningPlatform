namespace OnlineLearningPlatform.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;

        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;

        public UserRole Role { get; set; }

        public string? Avatar { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Course>? Courses { get; set; }
    }
}
