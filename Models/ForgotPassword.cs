using System.ComponentModel.DataAnnotations;

namespace BloodBank.Models
{
    public class ForgotPassword
    {
        [Key] // Add this attribute to define the primary key
        public int Id { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
    }
}
