using ItsaWeb.Infrastructure;

namespace ItsaWeb.Hubs
{
    public class SessionHub : AuthenticatingHub
    {
        public bool IsAuthenticatedUser()
        {
            try
            {
                return IsAuthenticated();
            }
            catch (NotLoggedInException)
            {
                return false;
            }            
        }
    }
}