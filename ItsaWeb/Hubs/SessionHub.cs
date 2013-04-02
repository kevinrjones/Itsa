namespace ItsaWeb.Hubs
{
    public class SessionHub : AuthenticatingHub
    {
        public bool IsAuthenticatedUser()
        {
            return IsAuthenticated();
        }
    }
}