namespace OnlineLearningPlatform.DTOs
{
    public class TeacherDashboardDto
    {
        public int CourseId { get; set; }
        public string Title { get; set; }
        public int StudentsCount { get; set; }
        public List<StudentDto> Students { get; set; }
    }
}
