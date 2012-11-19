using System;
using System.IO;
using System.Linq;
using SystemFileAdapter;
using Entities;
using FileRepository.Repositories;
using FileSystemInterfaces;
using FluentAssertions;
using NUnit.Framework;
using Serialization;

namespace FileRepositoryTests
{
    [TestFixture]
    public class BlogRepositoryTests
    {
        IFileInfoFactory _fileInfoFactory;
        private IDirectoryInfo _directoryInfo;
        BlogRepository _blogRepository;
        const string Directory = "files";
        FileInfo _file;

        [SetUp]
        public void Setup()
        {
            _fileInfoFactory = new SystemFileInfoFactory();
            _directoryInfo = new SystemIoDirectoryInfo();

            var di = new DirectoryInfo(Directory);
            if (di.Exists)
            {
                di.Delete(true);
            }
            di.Create();
            _blogRepository = new BlogRepository(Directory, _fileInfoFactory, _directoryInfo);
            CreateFiles(Directory);
        }

        [TearDown]
        public void TearDown()
        {
            var di = new DirectoryInfo(Directory);
            if (di.Exists)
            {
                di.Delete(true);
            }
        }

        private void CreateFiles(string directory)
        {
            for (int i = 0; i < 10; i++)
            {
                using (File.Create(Path.Combine(directory, "name" + i + ".json"))) ;
            }
        }

        [Test]
        public void GivenASetOfFilesOnDisk_WhenThoseFilesAreRetrieved_ThenTheFIlesAreReturned()
        {
            _blogRepository.Entities.Count().Should().Be(10);
        }

        [Test]
        public void GivenAnExistingFile_WhenAFileWithTheSameNameIsCreated_ThenAnExceptionIsThrown()
        {
            var entry = new BlogEntry { Id = new Guid(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1) };

            _file = new FileInfo(GenerateFileName(entry));
            using (_file.Create()) ;
            Action act = () => _blogRepository.Create(entry);
            act.ShouldThrow<Exception>();
        }

        [Test]
        public void GivenTheNameOfAFileThatDoesNotExist_WhenThatFileIsCreated_ThenTheFileExistsOnDisk()
        {
            var entry = new BlogEntry { Id = new Guid(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1) };

            _blogRepository.Create(entry);
            _file = new FileInfo(GenerateFileName(entry));
            _file.Exists.Should().BeTrue();
        }

        [Test]
        public void GivenTheNameOfAFileThatDoesNotExist_WhenThatFileIsCreated_ThenTheFileDataIsCorrect()
        {
            var entry = new BlogEntry { Title = "title", Id = new Guid(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1) };

            _blogRepository.Create(entry);
            _file = new FileInfo(GenerateFileName(entry));
            using (var stream = new StreamReader(_file.Open(FileMode.Open)))
            {
                var json = stream.ReadToEnd();
                entry = JsonSerializer.Deserialize<BlogEntry>(json);
                entry.Title.Should().Be("title");
            }
        }

        [Test]
        public void GivenTheNameOfAFileThatExists_WhenThatFileIsUpdated_ThenTheFileDataIsCorrect()
        {
            var entry = new BlogEntry { Title = "old title", Id = new Guid(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1) };

            _blogRepository.Create(entry);
            entry.Title = "new title";

            _blogRepository.Update(entry);
            _file = new FileInfo(GenerateFileName(entry));
            using (var stream = new StreamReader(_file.Open(FileMode.Open)))
            {
                var json = stream.ReadToEnd();
                entry = JsonSerializer.Deserialize<BlogEntry>(json);
                entry.Title.Should().Be("new title");
            }

        }

        [Test]
        public void GivenAnExistingFile_WhenTheFileIsDeleted_ThenTheFileIsRemovedFromDisk()
        {
            var entry = new BlogEntry { Title = "title", Id = new Guid(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1) };

            _file = new FileInfo(GenerateFileName(entry));
            using (_file.Create()) ;

            _blogRepository.Delete(entry);

            _file = new FileInfo(GenerateFileName(entry));

            _file.Exists.Should().BeFalse();
        }

        private string GenerateFileName(BlogEntry entity)
        {
            return string.Format("{0}/{1}.json", Directory, entity.Id);
        }

    }
}
