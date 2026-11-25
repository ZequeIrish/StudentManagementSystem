using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Controllers
{
    public class CoursesController : Controller
    {
        private readonly StudentManagementDbContext _context;

        public CoursesController(StudentManagementDbContext context)
        {
            _context = context;
        }

        // GET: Courses
        public async Task<IActionResult> Index()
        {
            return View(await _context.Courses.ToListAsync());
        }

        //create
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Course course)
        {
            if (string.IsNullOrWhiteSpace(course.CourseName))
            {
                return BadRequest("Course name is required.");
            }

            _context.Courses.Add(course);
            await _context.SaveChangesAsync();

            return Ok();
        }


        //edit
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [FromBody] Course course)
        {
            if (id != course.CourseId)
            {
                return BadRequest();
            }

            var existingCourse = await _context.Courses
                .FindAsync(id);

            if (existingCourse == null)
            {
                return NotFound();
            }

            if (string.IsNullOrWhiteSpace(course.CourseName))
            {
                return BadRequest("Course name is required.");
            }

            var newCourseName = course.CourseName.Trim();

            // Students save the course NAME (not its ID), so rename it on their records too.
            var studentsInCourse = await _context.Students
                .Where(s => s.Course == existingCourse.CourseName)
                .ToListAsync();

            foreach (var student in studentsInCourse)
            {
                student.Course = newCourseName;
            }

            existingCourse.CourseName = newCourseName;

            // One save = the course and its students change together, or not at all.
            await _context.SaveChangesAsync();

            return Ok();
        }

        //Delete
        [HttpPost]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var course = await _context.Courses.FindAsync(id);

            if (course == null)
            {
                return NotFound();
            }

            // Don't delete a course that students are still taking.
            var studentCount = await _context.Students
                .CountAsync(s => s.Course == course.CourseName);

            if (studentCount > 0)
            {
                return BadRequest(
                    $"Cannot delete \"{course.CourseName}\": {studentCount} student(s) are still in this course. " +
                    "Move them to another course first.");
            }

            _context.Courses.Remove(course);

            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}