using Microsoft.AspNetCore.Authorization;
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
    public class SubmissionsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SubmissionsController(AppDbContext context)
        {
            _context = context;
        }

        // STUDENT SUBMIT ASSIGNMENT
        [HttpPost("{assignmentId}/submit")]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> Submit(
            int assignmentId,
            SubmitAssignmentDto dto)
        {
            var studentIdClaim =
                User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (studentIdClaim == null)
                return Unauthorized("Invalid token");

            var studentId = int.Parse(studentIdClaim);

            var assignment =
                await _context.Assignments.FindAsync(assignmentId);

            if (assignment == null)
                return NotFound("Assignment not found");

            var existingSubmission = await _context.Submissions
                .FirstOrDefaultAsync(x =>
                    x.AssignmentId == assignmentId &&
                    x.StudentId == studentId);

            if (existingSubmission != null)
                return BadRequest("You already submitted this assignment");

            var submission = new Submission
            {
                AssignmentId = assignmentId,
                StudentId = studentId,
                Content = dto.Content,
                Score = null,
                Feedback = null
            };

            _context.Submissions.Add(submission);

            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Assignment submitted successfully"
            });
        }

        // TEACHER GET SUBMISSIONS
        [HttpGet("assignment/{assignmentId}")]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> GetByAssignment(int assignmentId)
        {
            var submissions = await _context.Submissions
                .Include(x => x.Student)
                .Include(x => x.Assignment)
                .Where(x => x.AssignmentId == assignmentId)
                .Select(x => new
                {
                    x.Id,
                    x.Content,
                    x.Score,
                    x.Feedback,
                    StudentEmail = x.Student.Email,
                    AssignmentTitle = x.Assignment.Title
                })
                .ToListAsync();

            return Ok(submissions);
        }

        // TEACHER GRADE SUBMISSION
        [HttpPut("{id}/grade")]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> GradeSubmission(
            int id,
            GradeSubmissionDto dto)
        {
            var submission = await _context.Submissions
                .FirstOrDefaultAsync(x => x.Id == id);

            if (submission == null)
                return NotFound("Submission not found");

            if (dto.Score < 0 || dto.Score > 100)
                return BadRequest("Score must be between 0 and 100");

            submission.Score = dto.Score;
            submission.Feedback = dto.Feedback;

            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Submission graded successfully"
            });
        }

        // STUDENT PROGRESS
        [HttpGet("progress")]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> GetMyProgress()
        {
            var studentId =
                int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var submissions = await _context.Submissions
                .Where(x => x.StudentId == studentId)
                .ToListAsync();

            var avgScore = submissions.Any()
                ? submissions.Average(x => x.Score ?? 0)
                : 0;

            return Ok(new
            {
                TotalSubmissions = submissions.Count,
                AverageScore = avgScore
            });
        }
    }
}