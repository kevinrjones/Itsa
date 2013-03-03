using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using FluentAssertions;
using ItsaWeb.Hubs;
using Moq;
using NUnit.Framework;
using ServiceInterfaces;

namespace BlogHubTests
{
    [TestFixture]
    public class GetBlogEntriesTests
    {
        
        Mock<IPostService> _service;

        [SetUp]
        public void Setup()
        {
            _service = new Mock<IPostService>();
        }

        [Test]
        public void GivenNoPosts_WhenPostsAreRequested_AnEmptyListIsReturned()
        {
            _service.Setup(s => s.GetPosts()).Returns(new List<Post>());
            BlogHub hub = new BlogHub(_service.Object);
            hub.GetBlogEntries().Count.Should().Be(0);
        }

        [Test]
        public void GivenPostsInTheRepository_WhenPostsAreRequested_AllPostsAreReturned()
        {
            var posts = new List<Post>
                            {
                                new Post {Body = "body1", Title = "title1"},
                                new Post {Body = "body2", Title = "title2"}
                            };
            _service.Setup(s => s.GetPosts()).Returns(posts);
            var hub = new BlogHub(_service.Object);
            hub.GetBlogEntries().Count.Should().Be(posts.Count);
        }
    }
}
