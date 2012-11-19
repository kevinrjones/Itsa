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
        private Mock<IBlogRepository> _blogRepository;
        private IAdminService _adminService;

        [SetUp]
        public void SetUp()
        {
            _blogRepository = new Mock<IBlogRepository>();
            _adminService = new AdminService(_blogRepository.Object);
        }

        [Test]
        public void GivenABlogEntry_WhenTheEntryIsAdded_ThenTheRepositoryIsCalled()
        {
            _adminService.AddBlogEntry(It.IsAny<BlogEntry>());
            _blogRepository.Verify(s => s.Create(It.IsAny<BlogEntry>()), Times.Once());

        }
    }
}
