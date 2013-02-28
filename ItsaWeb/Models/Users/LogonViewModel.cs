using System.ComponentModel.DataAnnotations;

namespace ItsaWeb.Models.Users
{
    public class LogonViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}