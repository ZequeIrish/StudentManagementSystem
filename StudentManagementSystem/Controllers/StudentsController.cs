using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Models;

public class StudentsController : Controller
{
    private readonly StudentManagementDbContext _context;

    public StudentsController(StudentManagementDbContext context)
    {
        _context = context;
    }

    // GET: STUDENTS
    public async Task<IActionResult> Index(string searchString, string sortOrder)
    {
        var students = _context.Students
            .Include(s => s.Department)
            .AsQueryable();

        if (!string.IsNullOrEmpty(searchString))
        {
            if (int.TryParse(searchString, out int age))
            {
                students = students.Where(s =>
                    s.Age == age ||
                    s.Address.Contains(searchString));
            }
            else
            {
                students = students.Where(s =>
                    s.FirstName.Contains(searchString) ||
                    s.LastName.Contains(searchString) ||
                    s.Course.Contains(searchString) ||
                    s.Address.Contains(searchString) ||
                    s.Department.DepartmentName.Contains(searchString));
            }
        }

        switch (sortOrder)
        {
            case "first_name":
                students = students.OrderBy(s => s.FirstName);
                break;

            case "last_name":
                students = students.OrderBy(s => s.LastName);
                break;

            case "course":
                students = students.OrderBy(s => s.Course);
                break;

            case "department":
                students = students.OrderBy(s => s.Department.DepartmentName);
                break;

            case "name_desc":
                students = students.OrderByDescending(s => s.LastName);
                break;

            default:
                students = students.OrderBy(s => s.LastName);
                break;
        }



        //Department Summary
            var departmentSummary = await _context.Departments
        .Select(d => new
        {
            d.DepartmentId,
            d.DepartmentName,
            StudentCount = _context.Students
                .Count(s => s.DepartmentId == d.DepartmentId)
        })
        .ToListAsync();

            ViewBag.DepartmentSummary = departmentSummary;


        //Average Age
        ViewBag.AverageAge = Math.Round(await _context.Students
        .AverageAsync(s => s.Age ?? 0));

            return View(await students.ToListAsync());
    }


    // GET: Students/DepartmentStudents/1
    public async Task<IActionResult> DepartmentStudents(int id)
    {
        var students = await _context.Students
            .Include(s => s.Department)
            .Where(s => s.DepartmentId == id)
            .ToListAsync();

        return Json(students.Select(s => new
        {
            studentId = s.StudentId,
            firstName = s.FirstName,
            middleName = s.MiddleName,
            lastName = s.LastName,
            age = s.Age,
            course = s.Course,
            address = s.Address,
            department = s.Department != null
                ? s.Department.DepartmentName
                : ""
        }));
    }
    // GET: STUDENTS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var student = await _context.Students
            .Include(s => s.Department)
            .FirstOrDefaultAsync(m => m.StudentId == id);

        if (student == null)
        {
            return NotFound();
        }

        return View(student);
    }

    // GET: STUDENTS/Create
    public async Task<IActionResult> Create()
    {
        ViewBag.Departments = await _context.Departments.ToListAsync();
        ViewBag.Courses = await _context.Courses.ToListAsync();

        return View();
    }

    // POST: STUDENTS/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        [Bind("StudentId,FirstName,MiddleName,LastName,Age,Course,Address,DepartmentId,Department")]
        Student student)
    {
        if (ModelState.IsValid)
        {
            _context.Add(student);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewBag.Departments = await _context.Departments.ToListAsync();

        return View(student);
    }

    // GET: STUDENTS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var student = await _context.Students.FindAsync(id);

        if (student == null)
        {
            return NotFound();
        }

        ViewBag.Departments = await _context.Departments.ToListAsync();
        ViewBag.Courses = await _context.Courses.ToListAsync();

        return View(student);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
    int? id,
    [Bind("StudentId,FirstName,MiddleName,LastName,Age,Course,Address,DepartmentId")] Student student)
    {
        if (id != student.StudentId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(student);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(student.StudentId))
                {
                    return NotFound();
                }

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        ViewBag.Departments = await _context.Departments.ToListAsync();
        ViewBag.Courses = await _context.Courses.ToListAsync();

        return View(student);
    }

    // GET: STUDENTS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var student = await _context.Students
            .Include(s => s.Department)
            .FirstOrDefaultAsync(m => m.StudentId == id);

        if (student == null)
        {
            return NotFound();
        }

        return View(student);
    }

    // POST: STUDENTS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var student = await _context.Students.FindAsync(id);

        if (student != null)
        {
            _context.Students.Remove(student);
        }

        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    private bool StudentExists(int? id)
    {
        return _context.Students.Any(e => e.StudentId == id);
    }
}