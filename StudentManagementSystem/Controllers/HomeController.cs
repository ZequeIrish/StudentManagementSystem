using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Models;
using System.Diagnostics;

namespace StudentManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly StudentManagementDbContext _context;

        public HomeController(
            ILogger<HomeController> logger,
            StudentManagementDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.TotalStudents = await _context.Students.CountAsync();

            ViewBag.TotalCourses = await _context.Courses.CountAsync();

            ViewBag.TotalDepartments = await _context.Departments.CountAsync();

            ViewBag.TotalAdmins = await _context.Admins.CountAsync();

            // The 5 newest students (a higher StudentId means added later).
            ViewBag.RecentStudents = await _context.Students
                .Include(s => s.Department)
                .OrderByDescending(s => s.StudentId)
                .Take(5)
                .ToListAsync();

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [AllowAnonymous]   // the error page must work even when not logged in
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
