using System.ComponentModel.DataAnnotations;

namespace BLL.Models.Authentication
{
    public class SignUpModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }
        public DateTime Birthday { get; set; }
        [Range(0, 2)]
        public int? Gender { get; set; }
        public string? Location { get; set; }
        [Range(0000000000, 9999999999)]
        public int? PhoneNumber { get; set; }
        public string? Image { get; set; }
    }
}
