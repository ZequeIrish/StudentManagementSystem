using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly StudentManagementDbContext _context;

        public DepartmentsController(StudentManagementDbContext context)
        {
            _context = context;
        }

        // GET: Departments
        public async Task<IActionResult> Index()
        {
            return View(await _context.Departments.ToListAsync());
        }

        //create
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Department department)
        {
            if (string.IsNullOrWhiteSpace(department.DepartmentName))
            {
                return BadRequest("Course name is required.");
            }

            _context.Departments.Add(department);
            await _context.SaveChangesAsync();

            return Ok();
        }


        //edit
        [HttpPost]
        public async Task<IActionResult> Edit(
    int id,
    [FromBody] Department department)
        {
            if (department == null)
            {
                return BadRequest("Department data is missing.");
            }

            if (id != department.DepartmentId)
            {
                return BadRequest("Department ID does not match.");
            }

            if (string.IsNullOrWhiteSpace(department.DepartmentName))
            {
                return BadRequest("Department name is required.");
            }

            var existingDepartment = await _context.Departments
                .FindAsync(id);

            if (existingDepartment == null)
            {
                return NotFound("Department not found.");
            }

            existingDepartment.DepartmentName =
                department.DepartmentName.Trim();

            await _context.SaveChangesAsync();

            return Ok();
        }

        //Delete
        [HttpPost]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            var department = await _context.Departments.FindAsync(id);

            if (department == null)
            {
                return NotFound();
            }

            _context.Departments.Remove(department);

            await _context.SaveChangesAsync();

            return Ok();
        }


    }
}