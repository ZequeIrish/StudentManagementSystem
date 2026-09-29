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

            existingCourse.CourseName = course.CourseName;

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

            _context.Courses.Remove(course);

            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}