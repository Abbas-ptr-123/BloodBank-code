using Microsoft.AspNetCore.Identity;

namespace BloodBank.Models
{
    public class User : IdentityUser
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? FullName { get; set; }
        public string? FatherName { get; set; }
        public string? ContactNumber { get; set; }
        public string? Address { get; set; }
        public string? BloodGroup { get; set; }
        public string Role { get; set; } // "Donor" or "Seeker"

    }
}
