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
using Repository;
using Serialization;

namespace FileRepositoryTests
{
    [TestFixture]
    public class BlogRepositoryTests
    {
        private Mock<IFileInfo> _fileInfo;
        private Mock<IDirectoryInfo> _directoryInfo;
        private Mock<IFileInfoFactory> _fileInfoFactory;
        private const string Path = "test";

        [SetUp]
        public void Setup()
        {
            _fileInfo = new Mock<IFileInfo>();
            _fileInfoFactory = new Mock<IFileInfoFactory>();
            _directoryInfo = new Mock<IDirectoryInfo>();
            _fileInfoFactory.Setup(f => f.CreateFileInfo(It.IsAny<string>())).Returns(_fileInfo.Object);
        }

        [Test]
        public void GivenABlogEntry_WhenThenFileIsCreated_ThenTheCorrectFileNameIsUsed()
        {
            string fileName = string.Format("{0}/{1}-{2}-{3}-{4}-{5}.json", Path, "title", 1990, 1, 1, new DateTime(1990, 1, 1).Ticks);
            _fileInfo.Setup(f => f.Create()).Returns(new MemoryStream());

            var entry = new Post { Title = "title", EntryAddedDate = new DateTime(1990, 1, 1) };
            var repository = new PostRepository(Path, _fileInfoFactory.Object, null);
            repository.Create(entry);
            _fileInfo.Verify(f => f.Create(), Times.Once());
        }

        [Test]
        public void GivenABlogEntry_WhenTheEntryIsWrittenToAFile_ThenTheCorrectJsonIsWritten()
        {
            var stream = new MemoryStream();
            _fileInfo.Setup(f => f.Create()).Returns(stream);
            var entry = new Post { Title = "title", EntryAddedDate = new DateTime(1990, 1, 1), EntryUpdateDate = new DateTime(1991, 2, 2), Body = "post" };
            var repository = new PostRepository(Path, _fileInfoFactory.Object, null);
            repository.Create(entry);
            var data = stream.ToArray();
            var json = Encoding.UTF8.GetString(data);
            var savedentry = JsonSerializer.Deserialize<Post>(json);
            savedentry.Title.Should().Be("title");
            savedentry.Body.Should().Be("post");
            savedentry.EntryAddedDate.Should().Be(new DateTime(1990, 1, 1));
            savedentry.EntryUpdateDate.Should().Be(new DateTime(1991, 2, 2));
        }

        [Test]
        public void GivenABlogEntry_WhenThenFileIsCreated_AndTheFileIsNotFound_ThenTheCorrectExceptionIsThrown()
        {
            _fileInfo.Setup(f => f.Create()).Throws(new IOException());
            var entry = new Post { Title = "title", EntryAddedDate = new DateTime(1990, 1, 1) };
            var repository = new PostRepository(Path, _fileInfoFactory.Object, null);
            Assert.Throws<RepositoryException>(() => repository.Create(entry));
        }

        [Test]
        public void GivenABlogEntry_WhenThenFileIsUdated_ThenTheCorrectFileIsFound()
        {
            string fileName = string.Format("{0}/{1}-{2}-{3}-{4}-{5}.json", Path, "title", 1990, 1, 1, new DateTime(1990, 1, 1).Ticks);
            _fileInfo.Setup(f => f.Open(FileMode.Open)).Returns(new MemoryStream());

            var entry = new Post { Title = "title", EntryAddedDate = new DateTime(1990, 1, 1) };
            var repository = new PostRepository(Path, _fileInfoFactory.Object, null);
            repository.Update(entry);
            _fileInfo.Verify(f => f.Open(FileMode.Open), Times.Once());
        }

        [Test]
        public void GivenABlogEntry_WhenTheEntryIsUpdatedInAFile_ThenTheCorrectJsonIsWritten()
        {
            var stream = new MemoryStream();
            _fileInfo.Setup(f => f.Open(FileMode.Open)).Returns(stream);
            var entry = new Post { Title = "title", EntryAddedDate = new DateTime(1990, 1, 1), EntryUpdateDate = new DateTime(1991, 2, 2), Body = "post" };
            var repository = new PostRepository(Path, _fileInfoFactory.Object, null);
            repository.Update(entry);
            var data = stream.ToArray();
            var json = Encoding.UTF8.GetString(data);
            var savedentry = JsonSerializer.Deserialize<Post>(json);
            savedentry.Title.Should().Be("title");
            savedentry.Body.Should().Be("post");
            savedentry.EntryAddedDate.Should().Be(new DateTime(1990, 1, 1));
            savedentry.EntryUpdateDate.Should().Be(new DateTime(1991, 2, 2));
        }

        [Test]
        public void GivenABlogEntry_WhenThenFileIsUdated_AndTheFileIsNotFound_ThenTheCorrectExceptionIsThrown()
        {
            _fileInfo.Setup(f => f.Open(FileMode.Open)).Throws(new IOException());
            var entry = new Post { Title = "title", EntryAddedDate = new DateTime(1990, 1, 1) };
            var repository = new PostRepository(Path, _fileInfoFactory.Object, null);
            Assert.Throws<RepositoryException>(() => repository.Update(entry));
        }

        [Test]
        public void GivenABlogEntry_WhenThenFileIsDeleted_ThenTheCorrectFileIsFound()
        {
            string fileName = string.Format("{0}/{1}-{2}-{3}-{4}-{5}.json", Path, "title", 1990, 1, 1, new DateTime(1990, 1, 1).Ticks);

            var entry = new Post { Title = "title", EntryAddedDate = new DateTime(1990, 1, 1) };
            var repository = new PostRepository(Path, _fileInfoFactory.Object, null);
            repository.Delete(entry);
            _fileInfo.Verify(f => f.Delete(), Times.Once());
        }

        [Test]
        public void GivenABlogEntry_WhenThenFileIsDeleted_AndTheFileIsNotFound_ThenTheCorrectExceptionIsThrown()
        {
            _fileInfo.Setup(f => f.Delete()).Throws(new IOException());
            var entry = new Post { Title = "title", EntryAddedDate = new DateTime(1990, 1, 1) };
            var repository = new PostRepository(Path, _fileInfoFactory.Object, null);
            Assert.Throws<RepositoryException>(() => repository.Delete(entry));
        }

        [Test]
        public void GivenACollectionOfSerializedBlogEntries_WhenTheCollectionIsRetrieved_ThenAllTheEntriesAreRetrieved()
        {
            MemoryStream stream1 = new MemoryStream();
            MemoryStream stream2 = new MemoryStream();
            var entry = new Post();
            var json = entry.SerializeToString();
            var data = Encoding.UTF8.GetBytes(json);
            stream1.Write(data, 0, data.Length);
            stream2.Write(data, 0, data.Length);
            var fileInfo1 = new Mock<IFileInfo>();
            fileInfo1.Setup(f => f.Open(FileMode.Open)).Returns(stream1);
            var fileInfo2 = new Mock<IFileInfo>();
            fileInfo2.Setup(f => f.Open(FileMode.Open)).Returns(stream2);
            var fileInfos = new List<IFileInfo> { fileInfo1.Object, fileInfo2.Object, };
            _directoryInfo.Setup(d => d.EnumerateFiles(It.IsAny<string>(), It.IsAny<string>())).Returns(fileInfos);
            var repository = new PostRepository(Path, _fileInfoFactory.Object, _directoryInfo.Object);
            var entities = repository.Entities;
            entities.Should().HaveCount(2);
        }
    }
}
