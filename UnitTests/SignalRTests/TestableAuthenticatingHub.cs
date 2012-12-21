using System.Security.Principal;
using ItsaWeb.Hubs;
using ItsaWeb.Models;
using Moq;
using SignalR;
using SignalR.Hubs;

namespace SignalRTests
{
    public class TestableAuthenticatingHub : AuthenticatingHub
    {
        public TestableAuthenticatingHub(string name)
        {
            InitializeHub(name);
        }

        private void InitializeHub(string name)
        {
            const string connectionId = "1234";
            const string hubName = "Authenticating";
            var mockConnection = new Mock<IConnection>();
            var mockRequest = new Mock<IRequest>();

            if (name != null)
            {
                var mockUser = new GenericPrincipal(new UserViewModel {Name = name}, null);
                mockRequest.Setup(r => r.User).Returns(mockUser);
            }
            var mockCookies = new Mock<IRequestCookieCollection>();

            mockRequest.Setup(r => r.Cookies).Returns(mockCookies.Object);

            Clients = new ClientProxy(mockConnection.Object, hubName);
            Context = new HubCallerContext(mockRequest.Object, connectionId);

            var trackingDictionary = new TrackingDictionary();
            Caller = new StatefulSignalProxy(mockConnection.Object, connectionId, hubName, trackingDictionary);
        }

        public string Identity
        {
            get { return UserName; }
        }
    }
}