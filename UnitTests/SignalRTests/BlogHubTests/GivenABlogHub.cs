using System;
using System.Collections.Generic;
using Entities;
using Exceptions;
using FluentAssertions;
using ItsaWeb.Hubs;
using ItsaWeb.Infrastructure;
using ItsaWeb.Models.Posts;
using Moq;
using NUnit.Framework;
using ServiceInterfaces;

namespace SignalRTests.BlogHubTests
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
            _service.Setup(s => s.GetAllPosts()).Returns(new List<Post>());
            var hub = new BlogHub(_service.Object);
            hub.List(true).Count.Should().Be(0);
        }

        [Test]
        public void WhenThereArePostsInTheRepository_AndPostsAreRequested_AllPostsAreReturned()
        {
            var posts = new List<Post>
                            {
                                new Post {Body = "body1", Title = "title1", Draft = true},
                                new Post {Body = "body2", Title = "title2", Draft = false},
                                new Post {Body = "body2", Title = "title2", Draft = false}
                            };
            _service.Setup(s => s.GetAllPosts()).Returns(posts);
            var hub = new BlogHub(_service.Object);
            hub.List(false).Count.Should().Be(posts.Count);
        }

        [Test]
        public void WhenThereArePostsThatAreDraftsInTheRepository_AndPostsAreRequested_AllPostsAreReturned()
        {
            var posts = new List<Post>
                            {
                                new Post {Body = "body1", Title = "title1", Draft = true},
                                new Post {Body = "body2", Title = "title2", Draft = false},
                                new Post {Body = "body2", Title = "title2", Draft = false}
                            };
            _service.Setup(s => s.GetAllPosts()).Returns(posts);
            var hub = new BlogHub(_service.Object);
            hub.List(true).Count.Should().Be(posts.Count);
        }

        [Test]
        public void WhenTheUserIsAuthenticated_AndAPostsIsCreated_ThenTheNewPostIsReturned()
        {
            _service.Setup(s => s.CreatePost(It.IsAny<Post>())).Returns(new Post());
            var hub = new TestableBlogHub(_service.Object, "Kevin");
            hub.Create(new BlogPostViewModel { Title = "title", Body = "body" });
            _service.Verify(s => s.CreatePost(It.IsAny<Post>()), Times.Once());
        }

        [Test]
        public void WhenTheUserIsAuthenticated_AndAPostsIsCreatedWithoutATitle_ThenAnExceptionIsThrown()
        {
            _service.Setup(s => s.CreatePost(It.IsAny<Post>())).Returns(new Post());
            var hub = new TestableBlogHub(_service.Object, "Kevin");
            Action act = () => hub.Create(new BlogPostViewModel { Body = "entry" });
            act.ShouldThrow<ItsaException>();
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
