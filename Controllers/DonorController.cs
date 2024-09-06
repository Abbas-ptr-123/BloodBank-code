using BloodBank.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBank.Controllers
{
    public class DonorController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<DonorController> _logger;

        public DonorController(UserManager<User> userManager, ILogger<DonorController> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Seeker")]
        public async Task<IActionResult> DonorList()
        {
            try
            {
                var donors = await _userManager.Users
                                               .Where(u => u.Role == "Donor")
                                               .Select(u => new Userlist
                                               {
                                                   FullName = u.FullName,
                                                   FatherName = u.FatherName,
                                                   ContactNumber = u.ContactNumber,
                                                   BloodGroup = u.BloodGroup
                                               }).ToListAsync();

                return View(donors);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching the donor list.");
                return View("Error");
            }
        }

        [Authorize(Roles = "Donor")]
        public async Task<IActionResult> Donors()
        {

            var username = User.Identity?.Name;
            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("Login", "Account");
            }

            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var seekers = await _userManager.Users
                                            .Where(u => u.Role == "Seeker")
                                            .Select(u => new Userlist
                                            {
                                                FullName = u.FullName,
                                                FatherName = u.FatherName,
                                                ContactNumber = u.ContactNumber,
                                                BloodGroup = u.BloodGroup
                                            }).ToListAsync();

            var viewModel = new Donor // Use the correct view model
            {
                User = user,
                Seekers = seekers
            };

            return View(viewModel);
        }
        //        catch (Exception ex)
        //        {
        //            _logger.LogError(ex, "An error occurred while fetching donor dashboard data.");
        //            return View("Error");
        //}

    }

}
