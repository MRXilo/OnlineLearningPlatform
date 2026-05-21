namespace OnlineLearningPlatform.DTOs
{
    public class StudentDashboardDto
    {
        public string CourseTitle { get; set; }
        public int TotalAssignments { get; set; }
        public int SubmittedAssignments { get; set; }
        public int Progress { get; set; } 
    }
}
