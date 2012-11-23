using System.Security.Principal;
using ItsaWeb.Authentication;

namespace ItsaWeb.Models
{
    public class UserViewModel : IItsaIdentity
    {
        public bool AllowComments { get; set; }
        public bool ModerateComments { get; set; }
        public string UserName { get; set; }
        public string BlogTitle { get; set; }
        public string BlogSubTitle { get; set; }
        public string Email { get; set; }

        public string Name { get; set; }
        public string AuthenticationType { get; set; }
        public bool IsAuthenticated { get; set; }

        public bool IsInRole(string role)
        {
            return false;
        }

    }
}