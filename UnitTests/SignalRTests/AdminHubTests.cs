using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Entities;
using FluentAssertions;
using ItsaWeb.Hubs;
using ItsaWeb.Infrastructure;
using ItsaWeb.Models;
using Moq;
using NUnit.Framework;
using ServiceInterfaces;
using SignalR;
using SignalR.Hubs;

namespace SignalRTests
{
    [TestFixture]
    class AdminHubTests
    {
        [Test]
        public void GivenAValidUser_WhenTheUserIsRequested_ThenTheUserIsReutrned()
        {
            var hub = new TestableAdminHub(null, "Kevin");
            UserViewModel model = hub.GetUser();
            model.Name.Should().Be("Kevin");
        }

        [Test]
        public void GivenAnInValidUser_WhenAPostIsSentToTheHub_ThenAnExceptionIsThrown()
        {
            var hub = new TestableAdminHub(null, null);
            Action act = () => hub.AddEntry(new BlogEntryViewModel());
            act.ShouldThrow<NotLoggedInException>();
        }

        [Test]
        public void GivenAValidUser_WhenAPostIsSentToTheHub_ThenThePostIsAdded()
        {
            Mock<IAdminService> service = new Mock<IAdminService>();
            var hub = new TestableAdminHub(service.Object, "Kevin");
            hub.AddEntry(new BlogEntryViewModel());

            service.Verify(s => s.AddBlogEntry(It.IsAny<Post>()), Times.Once());
        }
    }

    internal class TestableAdminHub : AdminHub
    {
        public TestableAdminHub(IAdminService adminService, string name)
            : base(adminService)
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
                var mockUser = new GenericPrincipal(new UserViewModel { Name = name }, null);
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
