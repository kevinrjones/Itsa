using System.ComponentModel.DataAnnotations;

namespace ItsaWeb.Models.Users
{
    public class RegisterUserViewModel
    {
        [Required]
        public string Password { get; set; }
        [Required]
        [Compare("Password", ErrorMessage = "Passwords must be the same")]
        public string RepeatPassword { get; set; }
        [Required]
        [UIHint("EmailAddress")]
        public string Email { get; set; }
    }
}