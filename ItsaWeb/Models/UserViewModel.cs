using System.Security.Principal;

namespace ItsaWeb.Models
{
    public class UserViewModel : IPrincipal
    {
        public bool AllowComments { get; set; }
        public bool ModerateComments { get; set; }
        public string UserName { get; set; }
        public string BlogTitle { get; set; }
        public string BlogSubTitle { get; set; }
        public bool IsInRole(string role)
        {
            return false;
        }

        public IIdentity Identity { get; set; }
    }
}