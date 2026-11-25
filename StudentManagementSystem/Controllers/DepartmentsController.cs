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
                return BadRequest("Department name is required.");
            }

            department.DepartmentName = department.DepartmentName.Trim();

            if (await DepartmentNameIsTaken(department.DepartmentName))
            {
                return BadRequest($"A department named \"{department.DepartmentName}\" already exists.");
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

            var newDepartmentName = department.DepartmentName.Trim();

            // Another department (not this one) already has this name?
            if (await DepartmentNameIsTaken(newDepartmentName, exceptDepartmentId: id))
            {
                return BadRequest($"A department named \"{newDepartmentName}\" already exists.");
            }

            existingDepartment.DepartmentName = newDepartmentName;

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

            // Don't delete a department that still has students.
            var studentCount = await _context.Students
                .CountAsync(s => s.DepartmentId == id);

            if (studentCount > 0)
            {
                return BadRequest(
                    $"Cannot delete \"{department.DepartmentName}\": {studentCount} student(s) are still in this department. " +
                    "Move them to another department first.");
            }

            _context.Departments.Remove(department);

            await _context.SaveChangesAsync();

            return Ok();
        }

        // True if a department already uses this name.
        // Capital letters and extra spaces are ignored.
        // When editing, pass the department's own ID so it does not count itself.
        private Task<bool> DepartmentNameIsTaken(string name, int? exceptDepartmentId = null)
        {
            var lowerName = name.Trim().ToLower();

            return _context.Departments.AnyAsync(d =>
                d.DepartmentName.Trim().ToLower() == lowerName &&
                d.DepartmentId != exceptDepartmentId);
        }
    }
}