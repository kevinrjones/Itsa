using ItsaWeb.Authentication;
using SignalR.Hubs;

namespace ItsaWeb.Hubs
{
    public abstract class AuthenticatingHub : Hub
    {
        protected string UserId
        {
            get
            {
                try
                {
                    return ((IItsaIdentity)Context.User.Identity).UserId;
                }
                catch
                {
                    throw new NotLoggedInException();
                }
            }
        }

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