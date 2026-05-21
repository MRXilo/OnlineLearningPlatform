using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineLearningPlatform.Data;
using OnlineLearningPlatform.DTOs;
using System.Security.Claims;

namespace OnlineLearningPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Student")]
    public class StudentController : ControllerBase
    {
        private readonly AppDbContext _context;

        public StudentController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("dashboard")]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> Dashboard()
        {
            var studentId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var data = await _context.Enrollments
                .Where(e => e.StudentId == studentId)
                .Select(e => new
                {
                    CourseTitle = e.Course.Title,

                    TotalAssignments = e.Course.Assignments.Count(),

                    SubmittedAssignments = e.Course.Assignments
                        .Count(a => a.Submissions.Any(s => s.StudentId == studentId)),

                    GradedAssignments = e.Course.Assignments
                        .Count(a => a.Submissions.Any(s => s.StudentId == studentId && s.Score != null)),

                    AverageScore = e.Course.Assignments
                        .SelectMany(a => a.Submissions
                            .Where(s => s.StudentId == studentId && s.Score != null))
                        .Select(s => (double?)s.Score)
                        .Average() ?? 0,

                    Progress = e.Course.Assignments.Count() == 0 ? 0 :
                        (e.Course.Assignments
                            .Count(a => a.Submissions.Any(s => s.StudentId == studentId)) * 100)
                        / e.Course.Assignments.Count()
                })
                .ToListAsync();

            return Ok(data);
        }
    }
}
