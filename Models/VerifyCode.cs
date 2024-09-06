using System.ComponentModel.DataAnnotations;

namespace BloodBank.Models
{
    public class VerifyCode
    {
      
        public int Id { get; set; }
        [Required]
        public string? Code { get; set; }
    }
}