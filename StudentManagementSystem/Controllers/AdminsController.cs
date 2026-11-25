using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Models;
using StudentManagementSystem.ViewModels;

namespace StudentManagementSystem.Controllers
{
    // Manage admin accounts. Only logged-in admins can open these pages (rule in Program.cs).
    public class AdminsController : Controller
    {
        private readonly StudentManagementDbContext _context;
        private readonly IPasswordHasher<Admin> _passwordHasher;

        public AdminsController(StudentManagementDbContext context, IPasswordHasher<Admin> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        // GET: /Admins
        public async Task<IActionResult> Index()
        {
            ViewBag.CurrentAdminId = GetCurrentAdminId();

            var admins = await _context.Admins
                .OrderBy(a => a.Username)
                .ToListAsync();

            return View(admins);
        }

        // GET: /Admins/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Admins/Create
        [HttpPost]
        public async Task<IActionResult> Create(SetupViewModel model)
        {
            // SetupViewModel has the same rules as the first admin: username format, 8+ character password, typed twice.
            var username = model.Username?.Trim() ?? "";

            if (await _context.Admins.AnyAsync(a => a.Username == username))
            {
                ModelState.AddModelError(nameof(model.Username), "This username is already taken.");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var admin = new Admin { Username = username };
            admin.PasswordHash = _passwordHasher.HashPassword(admin, model.Password);

            _context.Admins.Add(admin);
            await _context.SaveChangesAsync();

            TempData["Message"] = $"Admin \"{admin.Username}\" was added.";
            return RedirectToAction(nameof(Index));
        }

        // POST: /Admins/Delete/5
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            // You cannot delete yourself, so there is always at least one admin left.
            if (id == GetCurrentAdminId())
            {
                TempData["Error"] = "You cannot delete your own account.";
                return RedirectToAction(nameof(Index));
            }

            var admin = await _context.Admins.FindAsync(id);

            if (admin == null)
            {
                return NotFound();
            }

            _context.Admins.Remove(admin);
            await _context.SaveChangesAsync();

            TempData["Message"] = $"Admin \"{admin.Username}\" was deleted.";
            return RedirectToAction(nameof(Index));
        }

        // Reads the logged-in admin's ID from the login cookie (saved there by AccountController).
        private int? GetCurrentAdminId()
        {
            var value = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.TryParse(value, out var adminId) ? adminId : null;
        }
    }
}
