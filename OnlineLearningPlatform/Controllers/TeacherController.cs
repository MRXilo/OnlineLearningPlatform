using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineLearningPlatform.Data;
using System.Security.Claims;

namespace OnlineLearningPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Teacher")]
    public class TeacherController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TeacherController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("dashboard")]
        public async Task<IActionResult> GetDashboard()
        {
            var teacherId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var courses = await _context.Courses
                .Where(c => c.TeacherId == teacherId)
                .ToListAsync();

            var courseIds = courses.Select(c => c.Id).ToList();

            var assignmentsCount = await _context.Assignments
                .CountAsync(a => courseIds.Contains(a.CourseId));

            var submissionsCount = await _context.Submissions
                .CountAsync(s => courseIds.Contains(s.Assignment.CourseId));

            return Ok(new
            {
                TotalCourses = courses.Count,
                TotalAssignments = assignmentsCount,
                TotalSubmissions = submissionsCount
            });
        }
    }
}
