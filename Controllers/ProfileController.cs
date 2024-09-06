using BloodBank.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Text;

namespace BloodBank.Controllers
{
    public class ProfileController : Controller
    {
        private readonly PulseContext _context;
        private readonly PasswordHasher<User> _passwordHasher;

        public ProfileController(PulseContext context)
        {
            _context = context;
            _passwordHasher = new PasswordHasher<User>();
        }

      
        public IActionResult Update()
        {
            var username = User.Identity?.Name;

            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("LogIn", "Home");
            }

            var user = _context.Users.FirstOrDefault(u => u.Username == username);

            if (user == null)
            {
                return NotFound();
            }

            var model = new Profile
            {
                FullName = user.FullName,
                FatherName = user.FatherName,
                Email = user.Email,
                ContactNumber = user.ContactNumber,
                Address = user.Address,
                BloodGroup = user.BloodGroup
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Update(Profile model)
        {
            var username = User.Identity?.Name;

            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("LogIn", "Home");
            }

            var user = _context.Users.FirstOrDefault(u => u.Username == username);

            if (user == null)
            {
                return NotFound();
            }

            if (!string.IsNullOrEmpty(model.Password))
            {
                user.PasswordHash = _passwordHasher.HashPassword(user, model.Password);
            }

            user.FullName = model.FullName;
            user.FatherName = model.FatherName;
            user.Email = model.Email;
            user.ContactNumber = model.ContactNumber;
            user.Address = model.Address;
            user.BloodGroup = model.BloodGroup;

            _context.Users.Update(user);
            _context.SaveChanges();

            TempData["UpdateSuccess"] = "Profile updated successfully!";
            return RedirectToAction("Update");
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme).Wait();
            return RedirectToAction("LogIn", "Home");
        }
    }
}
