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
using ItsaWeb.Models.Posts;
using ItsaWeb.Models.Users;
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
        public void GivenAnInValidUser_WhenAPostIsSentToTheHub_ThenAnExceptionIsThrown()
        {
            var hub = new TestableAdminHub(null, null);
            Action act = () => hub.AddBlogPost(new NewPostViewModel());
            act.ShouldThrow<NotLoggedInException>();
        }

        [Test]
        public void GivenAValidUser_WhenAPostIsSentToTheHub_ThenThePostIsAdded()
        {
            var service = new Mock<IAdminService>();
            var hub = new TestableAdminHub(service.Object, "Kevin");
            service.Setup(s => s.AddBlogPost(It.IsAny<Post>())).Returns(new Post());
            hub.AddBlogPost(new NewPostViewModel());

            service.Verify(s => s.AddBlogPost(It.IsAny<Post>()), Times.Once());
        }

        [Test]
        public void GivenAValidUser_WhenAPostIsSentToTheHub_ThenTheCorrectDetailsAreSaved()
        {
            var service = new Mock<IAdminService>();
            var hub = new TestableAdminHub(service.Object, "Kevin");
            var post = new Post { Title = "title", Body = "body" };
            service.Setup(s => s.AddBlogPost(It.IsAny<Post>())).Returns(post);
            hub.AddBlogPost(new NewPostViewModel { Title = "title", Post = "body" });
            service.Verify(s => s.AddBlogPost(It.Is<Post>(p => p.Title == post.Title && p.Body == post.Body)), Times.Once());
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
