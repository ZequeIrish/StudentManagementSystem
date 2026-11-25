using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Models;
using StudentManagementSystem.ViewModels;

namespace StudentManagementSystem.Controllers
{
    // Login, logout, and first-time setup. These pages must work WITHOUT being logged in.
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly StudentManagementDbContext _context;
        private readonly IPasswordHasher<Admin> _passwordHasher;

        public AccountController(StudentManagementDbContext context, IPasswordHasher<Admin> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        // GET: /Account/Login
        public async Task<IActionResult> Login(string? returnUrl = null)
        {
            // Brand-new system with no admins yet: create the first one.
            if (!await _context.Admins.AnyAsync())
            {
                return RedirectToAction(nameof(Setup));
            }

            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
        {
            ViewBag.ReturnUrl = returnUrl;

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var admin = await _context.Admins
                .FirstOrDefaultAsync(a => a.Username == model.Username.Trim());

            var result = admin == null
                ? PasswordVerificationResult.Failed
                : _passwordHasher.VerifyHashedPassword(admin, admin.PasswordHash, model.Password);

            if (admin == null || result == PasswordVerificationResult.Failed)
            {
                // Same message for "wrong username" and "wrong password",
                // so nobody can find out which usernames exist.
                ModelState.AddModelError(string.Empty, "Invalid username or password.");
                return View(model);
            }

            // The hashing method got stronger since this password was saved: save a new hash.
            if (result == PasswordVerificationResult.SuccessRehashNeeded)
            {
                admin.PasswordHash = _passwordHasher.HashPassword(admin, model.Password);
                await _context.SaveChangesAsync();
            }

            await SignInAsync(admin);

            // Only go back to pages of THIS site (blocks links that send people to fake sites).
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return LocalRedirect(returnUrl);
            }

            return RedirectToAction("Index", "Home");
        }

        // POST: /Account/Logout
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(Login));
        }

        // GET: /Account/Setup
        public async Task<IActionResult> Setup()
        {
            // Setup is only allowed while there are NO admins. Otherwise anyone could make an account.
            if (await _context.Admins.AnyAsync())
            {
                return RedirectToAction(nameof(Login));
            }

            return View();
        }

        // POST: /Account/Setup
        [HttpPost]
        public async Task<IActionResult> Setup(SetupViewModel model)
        {
            if (await _context.Admins.AnyAsync())
            {
                return RedirectToAction(nameof(Login));
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var admin = new Admin { Username = model.Username.Trim() };
            admin.PasswordHash = _passwordHasher.HashPassword(admin, model.Password);

            _context.Admins.Add(admin);
            await _context.SaveChangesAsync();

            await SignInAsync(admin);

            return RedirectToAction("Index", "Home");
        }

        // Gives the browser a login cookie that says who this admin is.
        private async Task SignInAsync(Admin admin)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, admin.AdminId.ToString()),
                new Claim(ClaimTypes.Name, admin.Username)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity));
        }
    }
}
