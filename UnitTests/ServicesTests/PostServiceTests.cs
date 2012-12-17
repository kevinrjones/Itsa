using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using FluentAssertions;
using ItsaRepository.Interfaces;
using Moq;
using NUnit.Framework;
using Services;

namespace ServicesTests
{
    [TestFixture]
    public class PostServiceTests
    {
        private Mock<IPostRepository> _postRepository;
        private PostService _service;

        [SetUp]
        public void Setup()
        {
            _postRepository = new Mock<IPostRepository>();
            _service = new PostService(_postRepository.Object);
        }

        [Test]
        public void GivenAValidPost_WhenItIsAddedToTheService_ThenItIsAddedToTheRepository()
        {
            _service.CreatePost(It.IsAny<Post>());
            _postRepository.Verify(p => p.Create(It.IsAny<Post>()), Times.Once());
        }

        [Test]
        public void GivenAValidPost_WhenItIsRemovedFromTheService_ThenItIsDeletedInTheRepository()
        {
            var id = Guid.NewGuid();
            var posts = new List<Post> {new Post {Id = id}}.AsQueryable();
            _postRepository.Setup(p => p.Entities).Returns(posts);
            _service.DeletePost(id);
            _postRepository.Verify(p => p.Delete(It.Is<Post>(post => post.Id == id)), Times.Once());
        }

        [Test]
        public void GivenAValidPost_WhenItIsUpdatedInTheService_ThenItIsUpdatedInTheRepository()
        {
            var id = Guid.NewGuid();
            var posts = new List<Post> { new Post { Id = id } }.AsQueryable();
            _postRepository.Setup(p => p.Entities).Returns(posts);
            _service.UpdatePost(id, It.IsAny<string>(), It.IsAny<string>());
            _postRepository.Verify(p => p.Update(It.Is<Post>(post => post.Id == id)), Times.Once());
        }

        [Test]
        public void GivenAValidPost_WhenIRetrieveItFromTheService_ThenItIsRetrievedFromTheRepository()
        {
            var id = Guid.NewGuid();
            var posts = new List<Post> { new Post { Id = id } }.AsQueryable();
            _postRepository.Setup(p => p.Entities).Returns(posts);
            Post post = _service.GetPost(id);
            post.Id.Should().Be(id);
        }

        [Test]
        public void GivenASetOfPosts_WhenIRetrieveTheCount_ThenItIsTheCorrectCount()
        {
            var posts = new List<Post> { new Post(), new Post() }.AsQueryable();
            _postRepository.Setup(p => p.Entities).Returns(posts);
            var count = _service.GetCountOfPostsForBlog();
            count.Should().Be(2);
        }

    }
}
