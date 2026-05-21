using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineLearningPlatform.Data;
using OnlineLearningPlatform.DTOs;
using OnlineLearningPlatform.Models;

namespace OnlineLearningPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssignmentsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AssignmentsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> Create(int courseId, CreateAssignmentDto dto)
        {
            var course = await _context.Courses.FindAsync(courseId);

            if (course == null)
                return NotFound("Course not found");

            var assignment = new Assignment
            {
                CourseId = courseId,
                Title = dto.Title,
                Description = dto.Description,
                DueDate = dto.DueDate,
                MaxScore = dto.MaxScore
            };

            _context.Assignments.Add(assignment);
            await _context.SaveChangesAsync();

            return Ok(assignment);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var assignments = await _context.Assignments.ToListAsync();
            return Ok(assignments);
        }

        [HttpGet("course/{courseId}")]
        public async Task<IActionResult> GetByCourse(int courseId)
        {
            var assignments = await _context.Assignments
                .Where(a => a.CourseId == courseId)
                .ToListAsync();

            return Ok(assignments);
        }


    }
}
