using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using ItsaRepository.Interfaces;
using Moq;
using NUnit.Framework;
using ServiceInterfaces;
using Services;

namespace AdminServiceUnitTests
{
    [TestFixture]
    public class AdminServiceTests
    {
        private Mock<IPostRepository> _blogRepository;
        private IAdminService _adminService;

        [SetUp]
        public void SetUp()
        {
            _blogRepository = new Mock<IPostRepository>();
            _adminService = new AdminService(_blogRepository.Object);
        }

        [Test]
        public void GivenABlogEntry_WhenTheEntryIsAdded_ThenTheEntryIsAddedToTheRepository()
        {
            _adminService.AddBlogPost(It.IsAny<Post>());
            _blogRepository.Verify(s => s.Create(It.IsAny<Post>()), Times.Once());

        }

        [Test]
        public void GivenABlogEntry_WhenTheEntryIsDeleted_ThenTheEntryIsRemovedFromTheRepository()
        {
            var id = Guid.NewGuid();
            var post = new Post{Id = id};
            _blogRepository.Setup(b => b.Entities).Returns(new List<Post> { post }.AsQueryable());
            _adminService.DeleteBlogPost(id);
            _blogRepository.Verify(s => s.Delete(It.Is<Post>(p => p.Id == id)), Times.Once());
        }
    }
}
