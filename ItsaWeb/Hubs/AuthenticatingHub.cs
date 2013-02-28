using ItsaWeb.Authentication;
using ItsaWeb.Infrastructure;
using Microsoft.AspNet.SignalR;

namespace ItsaWeb.Hubs
{
    public abstract class AuthenticatingHub : Hub
    {
        protected bool IsAuthenticated()
        {
            return !string.IsNullOrEmpty(UserName);
        }

        protected string UserName
        {
            get
            {
                try
                {
                    return Context.User.Identity.Name;
                }
                catch
                {
                    throw new NotLoggedInException();
                }
            }
        }
    }
}