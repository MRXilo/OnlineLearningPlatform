using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineLearningPlatform.Data;
using OnlineLearningPlatform.DTOs;
using OnlineLearningPlatform.Models;
using System.Security.Claims;

namespace OnlineLearningPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CoursesController(AppDbContext context)
        {
            _context = context;
        }

        // GET ALL COURSES
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _context.Courses
                .Include(c => c.Teacher)
                .Select(c => new CourseDto
                {
                    Id = c.Id,
                    Title = c.Title,
                    TeacherName = c.Teacher.FirstName
                })
                .ToListAsync();

            return Ok(result);
        }

        //  GET COURSE BY ID
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var course = await _context.Courses
                .Include(c => c.Teacher)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (course == null)
                return NotFound();

            return Ok(course);
        }

        [HttpPost]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> CreateCourse(CreateCourseDto dto)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            var course = new Course
            {
                Title = dto.Title,
                Description = dto.Description,
                Category = dto.Category,
                Duration = dto.Duration,
                Price = dto.Price,
                TeacherId = int.Parse(userId)
            };

            _context.Courses.Add(course);
            await _context.SaveChangesAsync();

            return Ok(course);
        }

        // UPDATE COURSE
        [HttpPut("{id}")]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> Update(int id, UpdateCourseDto dto)
        {
            var course = await _context.Courses.FirstOrDefaultAsync(c => c.Id == id);

            if (course == null)
                return NotFound("Course not found");

            course.Title = dto.Title;
            course.Description = dto.Description;
            course.Category = dto.Category;
            course.Duration = dto.Duration;
            course.Price = dto.Price;

            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Course updated successfully",
                course.Id,
                course.Title
            });
        }

        //  DELETE
        [Authorize(Roles = "Teacher")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var course = await _context.Courses.FindAsync(id);

            if (course == null)
                return NotFound();

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("teacher/dashboard")]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> Dashboard()
        {
            var teacherId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var courses = await _context.Courses
                .Where(c => c.TeacherId == teacherId)
                .Include(c => c.Enrollments)
                    .ThenInclude(e => e.Student)
                .Select(c => new TeacherDashboardDto
                {
                    CourseId = c.Id,
                    Title = c.Title,
                    StudentsCount = c.Enrollments.Count,
                    

                    Students = c.Enrollments
                        .Select(e => new StudentDto
                        {
                            Id = e.Student.Id,
                            Email = e.Student.Email
                        })
                        .ToList()
                })
                .ToListAsync();

            return Ok(courses);
        
        }
    }
}
