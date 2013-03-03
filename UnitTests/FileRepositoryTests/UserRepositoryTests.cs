using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemFileAdapter;
using Entities;
using FileRepository.Repositories;
using FileSystemInterfaces;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace FileRepositoryTests
{
    [TestFixture]
    public class UserRepositoryTests
    {
        private UserRepository _repository;
        private Mock<IFileInfo> _fileInfo;
        private Mock<IDirectoryInfo> _directoryInfo;
        private Mock<IFileInfoFactory> _fileInfoFactory;

        [SetUp]
        public void Setup()
        {
            _fileInfo = new Mock<IFileInfo>();
            _fileInfoFactory = new Mock<IFileInfoFactory>();
            _directoryInfo = new Mock<IDirectoryInfo>();
            _fileInfoFactory.Setup(f => f.CreateFileInfo(It.IsAny<string>())).Returns(_fileInfo.Object);
            _repository = new UserRepository("path", _fileInfoFactory.Object, _directoryInfo.Object);
        }

        [Test]
        public void GivenAUserRepository_ThenItsFilenameShouldContainTheStringUser()
        {
            var repository = new MyUserRepository("foo", null, null);
            var fileName = repository.GeneratedFileName(new User());
            fileName.Should().Contain("user");
        }

        [Test]
        public void GivenAnExistingUser_WhenANewUserIsCreated_ThenTheExistingUserIsDestroyed()
        {
            _fileInfoFactory.Setup(f => f.CreateFileInfo(It.IsAny<string>())).Returns(_fileInfo.Object);
            _fileInfo.Setup(f => f.Exists).Returns(true);
            var stream = new Mock<Stream>();
            _fileInfo.Setup(f => f.Create()).Returns(stream.Object);
            _repository.Create(new User());
            _fileInfo.Verify(f => f.Delete(), Times.Once());
        }
    }

    public class MyUserRepository : UserRepository
    {
        public MyUserRepository(string path, IFileInfoFactory fileInfo, IDirectoryInfo directoryInfo) : base(path, fileInfo, directoryInfo)
        {
        }

        public string GeneratedFileName(User e)
        {
            return GenerateFileName(e);
        }
    }
}
