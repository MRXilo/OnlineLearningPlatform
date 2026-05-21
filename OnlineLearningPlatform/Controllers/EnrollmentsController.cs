using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineLearningPlatform.Data;
using OnlineLearningPlatform.Models;
using System.Security.Claims;

namespace OnlineLearningPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EnrollmentsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EnrollmentsController(AppDbContext context)
        {
            _context = context;
        }

        // =========================
        // ENROLL STUDENT
        // =========================
        [HttpPost("{courseId}")]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> Enroll(int courseId)
        {
            var studentId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var exists = await _context.Enrollments
                .AnyAsync(e => e.CourseId == courseId && e.StudentId == studentId);

            if (exists)
                return BadRequest("Already enrolled");

            var enrollment = new Enrollment
            {
                CourseId = courseId,
                StudentId = studentId,
                EnrolledAt = DateTime.UtcNow
            };

            _context.Enrollments.Add(enrollment);
            await _context.SaveChangesAsync();

            return Ok(enrollment);
        }

        // =========================
        // STUDENT DASHBOARD 
        // =========================
        [HttpGet("student/dashboard")]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> StudentDashboard()
        {
            var studentId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var enrolledCourses = await _context.Enrollments
                .Where(e => e.StudentId == studentId)
                .CountAsync();

            var completedAssignments = await _context.Submissions
                .CountAsync(s => s.StudentId == studentId && s.Score != null);

            var pendingAssignments = await _context.Submissions
                .CountAsync(s => s.StudentId == studentId && s.Score == null);

            return Ok(new
            {
                enrolledCourses,
                completedAssignments,
                pendingAssignments
            });
        }

        // =========================
        // GET MY COURSES
        // =========================
        [HttpGet("my")]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> MyCourses()
        {
            var studentId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var courses = await _context.Enrollments
                .Where(e => e.StudentId == studentId)
                .Include(e => e.Course)
                .Select(e => new
                {
                    e.Course.Id,
                    e.Course.Title,
                    e.Course.Description
                })
                .ToListAsync();

            return Ok(courses);
        }
    }
}