using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Entities;
using FluentAssertions;
using ItsaWeb.Hubs;
using ItsaWeb.Models;
using ItsaWeb.Models.Users;
using Moq;
using NUnit.Framework;
using ServiceInterfaces;
using SignalR;
using SignalR.Hubs;

namespace SignalRTests
{
    [TestFixture]
    class UserHubTests
    {
        Mock<IUserService> service = new Mock<IUserService>();

        [Test]
        public void GivenAnAuthenticatedUser_WhenTheUserIsRetrieved_ThenTheUserViewModelIsReturned()
        {
            service.Setup(s => s.GetUser()).Returns(new User());
            UserHub hub = new TestableUserHub(service.Object, "Kevin");
            hub.GetUser().Result.As<UserViewModel>().Should().NotBeNull();
        }

        [Test]
        public void GivenAnAuthenticatedUser_WhenTheUserIsRetrieved_ThenTheUserIsAuthenticated()
        {
            service.Setup(s => s.GetUser()).Returns(new User());
            UserHub hub = new TestableUserHub(service.Object, "Kevin");
            hub.GetUser().Result.As<UserViewModel>().IsAuthenticated.Should().BeTrue();
        }
    }

    internal class TestableUserHub : UserHub
    {
        public TestableUserHub(IUserService userService, string name) : base(userService)
        {
            InitializeHub(name);
        }

        private void InitializeHub(string name)
        {
            //const string connectionId = "1234";
            //const string hubName = "Authenticating";
            //var mockConnection = new Mock<IConnection>();
            var mockRequest = new Mock<IRequest>();

            if (name != null)
            {
                var mockUser = new GenericPrincipal(new UserViewModel { Name = name }, null);
                mockRequest.Setup(r => r.User).Returns(mockUser);
            }
            var mockCookies = new Mock<IRequestCookieCollection>();

            mockRequest.Setup(r => r.Cookies).Returns(mockCookies.Object);

            //Clients = new ClientProxy(mockConnection.Object, hubName);
            //Context = new HubCallerContext(mockRequest.Object, connectionId);

            var trackingDictionary = new TrackingDictionary();
            //Caller = new StatefulSignalProxy(mockConnection.Object, connectionId, hubName, trackingDictionary);
        }

        public string Identity
        {
            get { return UserName; }
        }
    }
}
