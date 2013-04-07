using System.Collections.Generic;
using System.Security.Principal;
using ItsaWeb.Hubs;
using ItsaWeb.Models.Users;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Moq;
using ServiceInterfaces;

namespace BlogHubTests
{
    public class TestableBlogHub : BlogHub
    {
        public TestableBlogHub(IPostService postService, string name)
            : base(postService)
        {
            InitializeHub(name);
        }

        private void InitializeHub(string name)
        {
            const string connectionId = "1234";
            //const string hubName = "Authenticating";
            //var mockConnection = new Mock<IConnection>();
            var mockRequest = new Mock<IRequest>();

            if (name != null)
            {
                var mockUser = new GenericPrincipal(new UserViewModel { Name = name }, null);
                mockRequest.Setup(r => r.User).Returns(mockUser);
            }

            mockRequest.Setup(r => r.Cookies).Returns(new Dictionary<string, Cookie>());

            //Clients = new ClientProxy(mockConnection.Object, hubName);
            Context = new HubCallerContext(mockRequest.Object, connectionId);

            //Caller = new StatefulSignalProxy(mockConnection.Object, connectionId, hubName, trackingDictionary);
        }

        public string Identity
        {
            get { return UserName; }
        }

    }
}