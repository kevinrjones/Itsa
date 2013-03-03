using Entities;
using ItsaWeb.Authentication;

namespace ItsaWeb.Models.Users
{
    public class UserViewModel : IItsaIdentity
    {
        public UserViewModel()
        {
            
        }

        public UserViewModel(User entityUser)
        {
            BlogTitle = entityUser.BlogTitle;
            IsAuthenticated = true;
            Email = entityUser.Email;
            Name = entityUser.Name;
            BlogSubTitle = entityUser.BlogDescription;
        }
        public bool AllowComments { get; set; }
        public bool ModerateComments { get; set; }
        public string BlogTitle { get; set; }
        public string BlogSubTitle { get; set; }
        public string Email { get; set; }

        public string Name { get; set; }
        public string AuthenticationType { get; private set; }
        public bool IsAuthenticated { get; set; }

        public bool IsInRole(string role)
        {
            return false;
        }

    }
}