using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineLearningPlatform.Data;
using OnlineLearningPlatform.DTOs;
using OnlineLearningPlatform.Models;

namespace OnlineLearningPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public LessonsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> CreateLesson(CreateLessonDto dto)
        {
            var course = await _context.Courses.FindAsync(dto.CourseId);

            if (course == null)
                return NotFound("Course not found");

            var lesson = new Lesson
            {
                Title = dto.Title,
                Content = dto.Content,
                OrderIndex = dto.OrderIndex,
                CourseId = dto.CourseId
            };

            _context.Lessons.Add(lesson);
            await _context.SaveChangesAsync();

            return Ok(lesson);
        }

        [HttpGet]
        public IActionResult GetLessons(int courseId)
        {
            var lessons = _context.Lessons
                .Where(x => x.CourseId == courseId)
                .OrderBy(x => x.OrderIndex)
                .ToList();

            return Ok(lessons);
        }
    }
}
