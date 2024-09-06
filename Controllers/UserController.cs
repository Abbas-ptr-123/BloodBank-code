using BloodBank.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using BCrypt.Net;

namespace BloodBank.Controllers
{
    public class UserController : Controller
    {
        private readonly PulseContext _context;

        public UserController(PulseContext context)
        {
            _context = context;
        }

        // User Registration
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {
            if (ModelState.IsValid)
            {
                // Hash the password before saving to the database
                user.PasswordHash = HashPassword(user.PasswordHash);

                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction("Login");
            }
            return View(user);
        }

        // User Login
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(User user)
        {
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == user.Username);

            if (existingUser != null && VerifyPassword(user.PasswordHash, existingUser.PasswordHash))
            {
                // Implement session or JWT token for login persistence
                return RedirectToAction("Dashboard", new { role = existingUser.Role });
            }

            ModelState.AddModelError("", "Invalid login attempt.");
            return View(user);
        }

        // Method to hash passwords
        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        // Method to verify passwords
        private bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }

        // Dashboard Redirection based on Role
        public IActionResult Dashboard(string role)
        {
            if (role == "Donor")
            {
                return RedirectToAction("Index", "Donor");
            }
            else if (role == "Seeker")
            {
                return RedirectToAction("Index", "Seeker");
            }

            return RedirectToAction("Login");
        }
    }
}
