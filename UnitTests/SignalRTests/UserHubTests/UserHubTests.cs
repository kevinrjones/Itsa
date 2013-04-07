using System.Security.Principal;
using Entities;
using FluentAssertions;
using ItsaWeb.Hubs;
using ItsaWeb.Models.Users;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Moq;
using NUnit.Framework;
using ServiceInterfaces;

namespace SignalRTests.UserHubTests
{
    [TestFixture]
    public class GivenAnAuthenticatedUser
    {
        Mock<IUserService> service = new Mock<IUserService>();

        [Test]
        public void WhenTheUserIsRetrieved_ThenTheUserViewModelIsReturned()
        {
            service.Setup(s => s.GetUser()).Returns(new User());
            UserHub hub = new TestableUserHub(service.Object, "Kevin");
            hub.GetUser().Result.As<UserViewModel>().Should().NotBeNull();
        }

        [Test]
        public void WhenTheUserIsRetrieved_ThenTheUserIsAuthenticated()
        {
            service.Setup(s => s.GetUser()).Returns(new User());
            UserHub hub = new TestableUserHub(service.Object, "Kevin");
            hub.GetUser().Result.As<UserViewModel>().IsAuthenticated.Should().BeTrue();
        }
    }

    [TestFixture]
    public class GivenAnUnAuthenticatedUser
    {
        Mock<IUserService> service = new Mock<IUserService>();

        [Test]
        public void WhenTheUserIsRetrieved_ThenNullIsReturned()
        {
            service.Setup(s => s.GetUser()).Returns((User)null);
            UserHub hub = new TestableUserHub(service.Object, null);
            hub.GetUser().Result.As<UserViewModel>().Should().BeNull();
        }
    }

    internal class TestableUserHub : UserHub
    {
        public TestableUserHub(IUserService userService, string name)
            : base(userService)
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
            //var mockCookies = new Mock<IRequestCookieCollection>();

            //mockRequest.Setup(r => r.Cookies).Returns(mockCookies.Object);

            //Clients = new ClientProxy(mockConnection.Object, hubName);
            Context = new HubCallerContext(mockRequest.Object, connectionId);

            //var trackingDictionary = new TrackingDictionary();
            //Caller = new StatefulSignalProxy(mockConnection.Object, connectionId, hubName, trackingDictionary);
        }

        public string Identity
        {
            get { return UserName; }
        }
    }
}