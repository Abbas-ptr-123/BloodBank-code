using System.ComponentModel.DataAnnotations;

namespace BloodBank.Models
{
    public class ContactUs
    {
            [Required]
            [StringLength(50)]
            public string Name { get; set; }

        public string MobileNo { get; set; }

        [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [StringLength(100)]
            public string Subject { get; set; }

            [Required]
            [StringLength(500)]
            public string Message { get; set; }
        }
    }



