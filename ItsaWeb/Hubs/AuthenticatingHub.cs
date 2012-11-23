using ItsaWeb.Authentication;
using ItsaWeb.Infrastructure;
using SignalR.Hubs;

namespace ItsaWeb.Hubs
{
    public abstract class AuthenticatingHub : Hub
    {
        protected string UserName
        {
            get
            {
                try
                {
                    return ((IItsaIdentity)Context.User.Identity).Name;
                }
                catch
                {
                    throw new NotLoggedInException();
                }
            }
        }
    }
}