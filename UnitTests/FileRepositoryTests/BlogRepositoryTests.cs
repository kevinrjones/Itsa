using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private const string Path = "test";

        [SetUp]
        public void Setup()
        {
            _fileInfo = new Mock<IFileInfo>();
        }

        [Test]
        public void GivenABlogEntry_WhenThenFileIsCreated_ThenTheCorrectFileNameIsUsed()
        {
            string fileName = string.Format("{0}/{1}-{2}-{3}-{4}-{5}.json", Path, "title", 1990, 1, 1, new DateTime(1990, 1, 1).Ticks);
            _fileInfo.Setup(f => f.Create(fileName)).Returns(new MemoryStream());

            var entry = new BlogEntry { Title = "title", EntryAddedDate = new DateTime(1990, 1, 1) };
            var repository = new BlogRepository(Path, _fileInfo.Object);
            repository.Create(entry);
            _fileInfo.Verify(f => f.Create(fileName), Times.Once());
        }

        [Test]
        public void GivenABlogEntry_WhenTheEntryIsWrittenToAFile_ThenTheCorrectJsonIsWritten()
        {
            var stream = new MemoryStream();
            _fileInfo.Setup(f => f.Create(It.IsAny<string>())).Returns(stream);
            var entry = new BlogEntry { Title = "title", EntryAddedDate = new DateTime(1990, 1, 1), EntryUpdateDate = new DateTime(1991, 2, 2), Post = "post" };
            var repository = new BlogRepository(Path, _fileInfo.Object);
            repository.Create(entry);
            var data = stream.ToArray();
            var json = Encoding.UTF8.GetString(data);
            var savedentry = JsonSerializer.Deserialize<BlogEntry>(json);
            savedentry.Title.Should().Be("title");
            savedentry.Post.Should().Be("post");
            savedentry.EntryAddedDate.Should().Be(new DateTime(1990, 1, 1));
            savedentry.EntryUpdateDate.Should().Be(new DateTime(1991, 2, 2));
        }

        [Test]
        public void GivenABlogEntry_WhenThenFileIsCreated_AndTheFileIsNotFound_ThenTheCorrectExceptionIsThrown()
        {
            _fileInfo.Setup(f => f.Create(It.IsAny<string>())).Throws(new IOException());
            var entry = new BlogEntry { Title = "title", EntryAddedDate = new DateTime(1990, 1, 1) };
            var repository = new BlogRepository(Path, _fileInfo.Object);
            Assert.Throws<RepositoryException>(() => repository.Create(entry));
        }

        [Test]
        public void GivenABlogEntry_WhenThenFileIsUdated_ThenTheCorrectFileIsFound()
        {
            string fileName = string.Format("{0}/{1}-{2}-{3}-{4}-{5}.json", Path, "title", 1990, 1, 1, new DateTime(1990, 1, 1).Ticks);
            _fileInfo.Setup(f => f.Open(FileMode.Open, fileName)).Returns(new MemoryStream());

            var entry = new BlogEntry { Title = "title", EntryAddedDate = new DateTime(1990, 1, 1) };
            var repository = new BlogRepository(Path, _fileInfo.Object);
            repository.Update(entry);
            _fileInfo.Verify(f => f.Open(FileMode.Open, fileName), Times.Once());
        }

        [Test]
        public void GivenABlogEntry_WhenTheEntryIsUpdatedInAFile_ThenTheCorrectJsonIsWritten()
        {
            var stream = new MemoryStream();
            _fileInfo.Setup(f => f.Open(FileMode.Open, It.IsAny<string>())).Returns(stream);
            var entry = new BlogEntry { Title = "title", EntryAddedDate = new DateTime(1990, 1, 1), EntryUpdateDate = new DateTime(1991, 2, 2), Post = "post" };
            var repository = new BlogRepository(Path, _fileInfo.Object);
            repository.Update(entry);
            var data = stream.ToArray();
            var json = Encoding.UTF8.GetString(data);
            var savedentry = JsonSerializer.Deserialize<BlogEntry>(json);
            savedentry.Title.Should().Be("title");
            savedentry.Post.Should().Be("post");
            savedentry.EntryAddedDate.Should().Be(new DateTime(1990, 1, 1));
            savedentry.EntryUpdateDate.Should().Be(new DateTime(1991, 2, 2));
        }

        [Test]
        public void GivenABlogEntry_WhenThenFileIsUdated_AndTheFileIsNotFound_ThenTheCorrectExceptionIsThrown()
        {
            _fileInfo.Setup(f => f.Open(FileMode.Open, It.IsAny<string>())).Throws(new IOException());
            var entry = new BlogEntry { Title = "title", EntryAddedDate = new DateTime(1990, 1, 1) };
            var repository = new BlogRepository(Path, _fileInfo.Object);
            Assert.Throws<RepositoryException>(() => repository.Update(entry));
        }

        [Test]
        public void GivenABlogEntry_WhenThenFileIsDeleted_ThenTheCorrectFileIsFound()
        {
            string fileName = string.Format("{0}/{1}-{2}-{3}-{4}-{5}.json", Path, "title", 1990, 1, 1, new DateTime(1990, 1, 1).Ticks);

            var entry = new BlogEntry { Title = "title", EntryAddedDate = new DateTime(1990, 1, 1) };
            var repository = new BlogRepository(Path, _fileInfo.Object);
            repository.Delete(entry);
            _fileInfo.Verify(f => f.Delete(fileName), Times.Once());
        }

        [Test]
        public void GivenABlogEntry_WhenThenFileIsDeleted_AndTheFileIsNotFound_ThenTheCorrectExceptionIsThrown()
        {
            _fileInfo.Setup(f => f.Delete(It.IsAny<string>())).Throws(new IOException());
            var entry = new BlogEntry { Title = "title", EntryAddedDate = new DateTime(1990, 1, 1) };
            var repository = new BlogRepository(Path, _fileInfo.Object);
            Assert.Throws<RepositoryException>(() => repository.Delete(entry));
        }

    }
}
