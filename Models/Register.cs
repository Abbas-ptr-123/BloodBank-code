using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BloodBank.Models
{
    public class Register
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Username is required.")] 
        public string? Username { get; set; }
        [Required(ErrorMessage = "FullName is required.")]
        public string? FullName { get; set; }
        [Required(ErrorMessage = "FatherName is required.")]
        public string? FatherName { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        public string? Password { get; set; }
        public string? ContactNumber { get; set; }
        public string? Address { get; set; }
        public string? BloodGroup { get; set; }
        public string? Role { get; set; } // Donor or Seeker
   
   
    }
}
