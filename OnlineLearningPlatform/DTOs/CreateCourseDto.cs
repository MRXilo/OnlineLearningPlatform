namespace OnlineLearningPlatform.DTOs
{
    public class CreateCourseDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public int Duration { get; set; }
        public decimal Price { get; set; }
    }
}
