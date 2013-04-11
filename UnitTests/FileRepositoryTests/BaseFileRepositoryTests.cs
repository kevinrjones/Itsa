using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemFileAdapter;
using Entities;
using Exceptions;
using FileRepository.Repositories;
using FileSystemInterfaces;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Repository;
using Serialization;

namespace FileRepositoryTests
{
    [TestFixture]
    public class BaseFileRepositoryTests
    {
        private PostRepository _repository;
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
            _repository = new PostRepository("path", _fileInfoFactory.Object, _directoryInfo.Object);
        }

        [Test]
        public void WhenAnEntityIsNewed_ThenANewEntityIsReturned()
        {
            var post = _repository.New();

            post.Should().NotBeNull();
        }

        [Test]
        public void WhenAnEntityIsCreated_ThenANewEntityIsWritenToFile()
        {
            var stream = new Mock<Stream>();
            _fileInfo.Setup(f => f.Create()).Returns(stream.Object);
            _fileInfoFactory.Setup(f => f.CreateFileInfo(It.IsAny<string>())).Returns(_fileInfo.Object);
            var post = new Post { Id = Guid.NewGuid() };
            _repository.Create(post);

            stream.Verify(s => s.Write(It.IsAny<byte[]>(), It.IsAny<int>(), It.IsAny<int>()), Times.AtLeastOnce());
        }

        [Test]
        public void WhenAnEntityIsDeleted_ThenTheEntityIsDeletedFromTheFile()
        {
            _fileInfoFactory.Setup(f => f.CreateFileInfo(It.IsAny<string>())).Returns(_fileInfo.Object);
            var post = new Post { Id = Guid.NewGuid() };
            _repository.Delete(post);

            _fileInfo.Verify(s => s.Delete(), Times.AtLeastOnce());
        }

        [Test]
        public void WhenAnEntityIsUpdated_ThenTheEntityIsWritenToTheFile()
        {
            var stream = new Mock<Stream>();
            _fileInfo.Setup(f => f.Open(FileMode.Truncate)).Returns(stream.Object);
            _fileInfoFactory.Setup(f => f.CreateFileInfo(It.IsAny<string>())).Returns(_fileInfo.Object);
            Post post = new Post { Id = Guid.NewGuid() };
            _repository.Update(post);

            stream.Verify(s => s.Write(It.IsAny<byte[]>(), It.IsAny<int>(), It.IsAny<int>()), Times.AtLeastOnce());
        }

        [Test]
        public void WhenEntitiesAreRetrievedFromTheRepository_ThenAllEntitiesAreReturned()
        {
            var streams = new MemoryStream[] { new MemoryStream(), new MemoryStream(), new MemoryStream()};
            foreach (var stream in streams)
            {
                var p = new Post();
                var json = p.SerializeToString();
                var bytes = Encoding.UTF8.GetBytes(json);
                stream.Write(bytes, 0, bytes.Length);
            }
            _fileInfo.Setup(f => f.Open(FileMode.Open)).Returns(streams[0]);
            var fileInfo1 = new Mock<IFileInfo>();
            fileInfo1.Setup(f => f.Open(FileMode.Open)).Returns(streams[1]);
            var fileInfo2 = new Mock<IFileInfo>();
            fileInfo2.Setup(f => f.Open(FileMode.Open)).Returns(streams[2]);

            _directoryInfo.Setup(d => d.EnumerateFiles(It.IsAny<string>(), It.IsAny<string>()))
                          .Returns(new List<IFileInfo> { _fileInfo.Object, fileInfo1.Object, fileInfo2.Object });
            _fileInfoFactory.Setup(f => f.CreateFileInfo(It.IsAny<string>())).Returns(_fileInfo.Object);
            var posts = _repository.Entities;

            posts.Count().Should().Be(3);
            _repository.Dispose();
        }

        [Test]
        public void WhenAnEntityIsCreated_AndAnIoExceptionIsThrown_ThenAnItsaExceptionIsRethrown()
        {
            _fileInfoFactory.Setup(f => f.CreateFileInfo(It.IsAny<string>())).Throws<IOException>();
            Post post = new Post { Id = Guid.NewGuid() };
            Action act = () => _repository.Create(post);

            act.ShouldThrow<RepositoryException>();
        }

        [Test]
        public void WhenAnEntityIsCreated_AndAnExceptionIsThrown_ThenAnItsaExceptionIsRethrown()
        {
            _fileInfoFactory.Setup(f => f.CreateFileInfo(It.IsAny<string>())).Throws<Exception>();
            Post post = new Post { Id = Guid.NewGuid() };
            Action act = () => _repository.Create(post);

            act.ShouldThrow<ItsaException>();
        }

        [Test]
        public void WhenAnEntityIsUpdated_AndAnExceptionIsThrown_ThenAnItsaExceptionIsRethrown()
        {
            _fileInfoFactory.Setup(f => f.CreateFileInfo(It.IsAny<string>())).Throws<Exception>();
            Post post = new Post { Id = Guid.NewGuid() };
            Action act = () => _repository.Update(post);

            act.ShouldThrow<ItsaException>();
        }

        [Test]
        public void WhenAnEntityIsUpdated_AndAnIoExceptionIsThrown_ThenARepositoryExceptionIsRethrown()
        {
            _fileInfoFactory.Setup(f => f.CreateFileInfo(It.IsAny<string>())).Throws<IOException>();
            Post post = new Post { Id = Guid.NewGuid() };
            Action act = () => _repository.Update(post);

            act.ShouldThrow<RepositoryException>();
        }
        
        [Test]
        public void WhenAnEntityIsDeleted_AndAnExceptionIsThrown_ThenAnItsaExceptionIsRethrown()
        {
            _fileInfoFactory.Setup(f => f.CreateFileInfo(It.IsAny<string>())).Throws<Exception>();
            Post post = new Post { Id = Guid.NewGuid() };
            Action act = () => _repository.Delete(post);

            act.ShouldThrow<ItsaException>();
        }

        [Test]
        public void WhenAnEntityIsDeleted_AndAnIOExceptionIsThrown_ThenARepositoryExceptionIsRethrown()
        {
            _fileInfoFactory.Setup(f => f.CreateFileInfo(It.IsAny<string>())).Throws<IOException>();
            Post post = new Post { Id = Guid.NewGuid() };
            Action act = () => _repository.Delete(post);

            act.ShouldThrow<RepositoryException>();
        }
    }
}
