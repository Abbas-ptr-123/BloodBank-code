using System.ComponentModel.DataAnnotations;

namespace BloodBank.Models
{
    public class Profile
    {
        public int Id { get; set; }

        [Required]
        public string? FullName { get; set; }

        public string? FatherName { get; set; }

        [Required, EmailAddress]
        public string? Email { get; set; }
        public string Password { get; set; }

        [Phone]
        public string? ContactNumber { get; set; }

        public string? Address { get; set; }

        public string? BloodGroup { get; set; }
    }
}
