using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using FluentAssertions;
using ItsaWeb.Hubs;
using ItsaWeb.Infrastructure;
using ItsaWeb.Models.Posts;
using Moq;
using NUnit.Framework;
using ServiceInterfaces;

namespace BlogHubTests
{
    [TestFixture]
    public class GivenABlogHub
    {

        Mock<IPostService> _service;

        [SetUp]
        public void Setup()
        {
            _service = new Mock<IPostService>();
        }

        [Test]
        public void WhenThereAreNoPosts_AndPostsAreRequested_AnEmptyListIsReturned()
        {
            _service.Setup(s => s.GetPosts()).Returns(new List<Post>());
            var hub = new BlogHub(_service.Object);
            hub.List().Count.Should().Be(0);
        }

        [Test]
        public void WhenThereArePostsInTheRepository_AndPostsAreRequested_AllPostsAreReturned()
        {
            var posts = new List<Post>
                            {
                                new Post {Body = "body1", Title = "title1"},
                                new Post {Body = "body2", Title = "title2"}
                            };
            _service.Setup(s => s.GetPosts()).Returns(posts);
            var hub = new BlogHub(_service.Object);
            hub.List().Count.Should().Be(posts.Count);
        }

        [Test]
        public void WhenTheUserIsAuthenticated_AndAPostsIsCreate_ThenTheNewPostIsReturned()
        {
            _service.Setup(s => s.CreatePost(It.IsAny<Post>()));
            var hub = new BlogHub(_service.Object);
            hub.Create(It.IsAny<BlogPostViewModel>());
            _service.Verify(s => s.CreatePost(It.IsAny<Post>()), Times.Once());
        }

        [Test]
        public void WhenTheUserNotIsAuthenticated_AndAPostsIsCreate_ThenAnExceptionIsThrown()
        {
            var hub = new BlogHub(_service.Object);
            Action act = () => hub.Create(new BlogPostViewModel());
            act.ShouldThrow<NotLoggedInException>();
        }

    }
}
