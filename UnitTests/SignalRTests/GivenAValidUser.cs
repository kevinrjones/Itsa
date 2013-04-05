using System;
using Entities;
using ItsaWeb.Models.Posts;
using Moq;
using NUnit.Framework;
using ServiceInterfaces;

namespace SignalRTests.AdminHubTests
{
    [TestFixture]
    internal class GivenAValidUser
    {
        [Test]
        public void WhenAPostIsSentToTheHub_ThenThePostIsAdded()
        {
            var service = new Mock<IAdminService>();
            var hub = new TestableAdminHub(service.Object, "Kevin");
            service.Setup(s => s.AddBlogPost(It.IsAny<Post>())).Returns(new Post());
            hub.AddBlogPost(new NewPostViewModel());

            service.Verify(s => s.AddBlogPost(It.IsAny<Post>()), Times.Once());
        }

        [Test]
        public void WhenAPostIsSentToTheHub_ThenTheCorrectDetailsAreSaved()
        {
            var service = new Mock<IAdminService>();
            var hub = new TestableAdminHub(service.Object, "Kevin");
            var post = new Post { Title = "title", Body = "body" };
            service.Setup(s => s.AddBlogPost(It.IsAny<Post>())).Returns(post);
            hub.AddBlogPost(new NewPostViewModel { Title = "title", Post = "body" });
            service.Verify(s => s.AddBlogPost(It.Is<Post>(p => p.Title == post.Title && p.Body == post.Body)),
                           Times.Once());
        }

        [Test]
        public void WhenAPostIsDeleted_ThenThePostIsDeletedFromTheService()
        {
            var service = new Mock<IAdminService>();
            var hub = new TestableAdminHub(service.Object, "Kevin");
            hub.DeleteBlogPost(It.IsAny<Guid>());

            service.Verify(s => s.DeleteBlogPost(It.IsAny<Guid>()), Times.Once());
        }
    }
}