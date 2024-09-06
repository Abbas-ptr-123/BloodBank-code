using BloodBank.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;


namespace BloodBank.Controllers
{
    public class SeekerController : Controller
    {
        private readonly PulseContext _context;
        private readonly PasswordHasher<User> _passwordHasher;
        private readonly ILogger<SeekerController> _logger;

        public SeekerController(PulseContext context, ILogger<SeekerController> logger)
        {
            _context = context;
            _passwordHasher = new PasswordHasher<User>();
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "Donor")]
        public async Task<IActionResult> SeekerList()
        {
            var seekers = await _context.Users
                                        .Where(u => u.Role == "Seeker")
                                        .Select(u => new Userlist
                                        {
                                            FullName = u.FullName,
                                            FatherName = u.FatherName,
                                            ContactNumber = u.ContactNumber,
                                            BloodGroup = u.BloodGroup
                                        }).ToListAsync();

            return View(seekers);
        }
        public IActionResult Seekers()
        {
            return View();
        }
        [Authorize(Roles = "Seeker")]
        public async Task<IActionResult> SeekerDashboard()
        {
            // Get the username of the currently logged-in user
            var username = User.Identity?.Name;
            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("LogIn");
            }

            // Get the current user
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Username == username);

            // Retrieve donors (for the seeker dashboard)
            var donors = await _context.Users
                                       .Where(u => u.Role == "Donor")
                                       .Select(u => new Userlist
                                       {
                                           FullName = u.FullName,
                                           FatherName = u.FatherName,
                                           ContactNumber = u.ContactNumber,
                                           BloodGroup = u.BloodGroup // Assuming this property exists
                                       }).ToListAsync();

            // Create a view model for the dashboard
            var viewModel = new Seeker
            {
                User = user,
                Donors = donors
            };

            return View(viewModel); // Pass the view model to the view
        }
    }
}
