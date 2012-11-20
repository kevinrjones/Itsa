using System.ComponentModel.DataAnnotations;

namespace ItsaWeb.Models
{
    public class LogonViewModel
    {
        [Display(Name = "User Name")]
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }

        public string RedirectTo { get; set; }

    }

    public class RegisterUserViewModel
    {
        [Display(Name = "User Name")]
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        public string Email { get; set; }
    }
}