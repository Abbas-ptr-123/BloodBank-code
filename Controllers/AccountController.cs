using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using BloodBank.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;

namespace BloodBank.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly PulseContext _context;
        private readonly ILogger<AccountController> _logger;

        public AccountController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IEmailSender emailSender,
            PulseContext context,
            ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _context = context;
            _logger = logger;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Register model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var newUser = new User
                    {
                        UserName = model.Username, // Changed to UserName
                        Email = model.Email,
                        FullName = model.FullName,
                        FatherName = model.FatherName,
                        ContactNumber = model.ContactNumber,
                        BloodGroup = model.BloodGroup,
                        Address = model.Address,
                        Role = model.Role,
                    };

                    var result = await _userManager.CreateAsync(newUser, model.Password);

                    if (result.Succeeded)
                    {
                        // Optionally send email confirmation
                        // var code = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
                        // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = newUser.Id, code }, protocol: Request.Scheme);
                        // await _emailSender.SendEmailAsync(model.Email, "Confirm your email", 
                        //     $"Please confirm your account by clicking this link: <a href='{callbackUrl}'>link</a>");

                        return RedirectToAction("Login");
                    }

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while registering the user.");
                    ModelState.AddModelError(string.Empty, "An error occurred while processing your request.");
                }
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LogIn model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.Username);

                if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
                {
                    await _signInManager.SignInAsync(user, model.RememberMe);

                    if (await _userManager.IsInRoleAsync(user, "Donor"))
                    {
                        return RedirectToAction("Donors", "Donor");
                    }
                    else if (await _userManager.IsInRoleAsync(user, "Seeker"))
                    {
                        return RedirectToAction("Seekers", "Seeker");
                    }
                    else
                    {
                        return View("Error");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
