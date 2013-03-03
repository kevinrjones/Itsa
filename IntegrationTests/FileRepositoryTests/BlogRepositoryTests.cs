using System;
using System.IO;
using System.Linq;
using System.Threading;
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
        PostRepository _postRepository;
        const string BaseDirectory = "files";
        const string PostsDirectory = "files/posts";
        FileInfo _file;

        [SetUp]
        public void Setup()
        {
            _fileInfoFactory = new SystemFileInfoFactory();
            _directoryInfo = new SystemIoDirectoryInfo();

            var di = new DirectoryInfo(PostsDirectory);
            if (di.Exists)
            {
                di.Delete(true);
            }
            di.Create();
            _postRepository = new PostRepository(BaseDirectory, _fileInfoFactory, _directoryInfo);
            CreateFiles(PostsDirectory);
        }

        [TearDown]
        public void TearDown()
        {
            var di = new DirectoryInfo(PostsDirectory);
            while (di.Exists)
            {
                try
                {
                    di.Delete(true);
                }
                catch
                {
                }
                Thread.Sleep(10);
                di = new DirectoryInfo(PostsDirectory);
            }
            di = new DirectoryInfo(BaseDirectory);
            while (di.Exists)
            {
                try
                {
                    di.Delete(true);
                }
                catch
                {
                }
                Thread.Sleep(10);
                di = new DirectoryInfo(BaseDirectory);
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
            _postRepository.Entities.Count().Should().Be(10);
        }

        [Test]
        public void GivenAnExistingFile_WhenAFileWithTheSameNameIsCreated_ThenAnExceptionIsThrown()
        {
            var entry = new Post { Id = new Guid(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1) };

            _file = new FileInfo(GenerateFileName(entry));
            using (_file.Create()) ;
            Action act = () => _postRepository.Create(entry);
            act.ShouldThrow<Exception>();
        }

        [Test]
        public void GivenTheNameOfAFileThatDoesNotExist_WhenThatFileIsCreated_ThenTheFileExistsOnDisk()
        {
            var entry = new Post { Id = new Guid(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1) };

            _postRepository.Create(entry);
            _file = new FileInfo(GenerateFileName(entry));
            _file.Exists.Should().BeTrue();
        }

        [Test]
        public void GivenTheNameOfAFileThatDoesNotExist_WhenThatFileIsCreated_ThenTheFileDataIsCorrect()
        {
            var entry = new Post { Title = "title", Id = new Guid(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1) };

            _postRepository.Create(entry);
            _file = new FileInfo(GenerateFileName(entry));
            using (var stream = new StreamReader(_file.Open(FileMode.Open)))
            {
                var json = stream.ReadToEnd();
                entry = JsonSerializer.Deserialize<Post>(json);
                entry.Title.Should().Be("title");
            }
        }

        [Test]
        public void GivenTheNameOfAFileThatExists_WhenThatFileIsUpdated_ThenTheFileDataIsCorrect()
        {
            var entry = new Post { Title = "old title", Id = new Guid(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1) };

            _postRepository.Create(entry);
            entry.Title = "new title";

            _postRepository.Update(entry);
            _file = new FileInfo(GenerateFileName(entry));
            using (var stream = new StreamReader(_file.Open(FileMode.Open)))
            {
                var json = stream.ReadToEnd();
                entry = JsonSerializer.Deserialize<Post>(json);
                entry.Title.Should().Be("new title");
            }

        }

        [Test]
        public void GivenAnExistingFile_WhenTheFileIsDeleted_ThenTheFileIsRemovedFromDisk()
        {
            var entry = new Post { Title = "title", Id = new Guid(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1) };

            _file = new FileInfo(GenerateFileName(entry));
            using (_file.Create()){};

            _postRepository.Delete(entry);

            _file = new FileInfo(GenerateFileName(entry));

            _file.Exists.Should().BeFalse();
        }

        private string GenerateFileName(Post entity)
        {
            return string.Format("{0}/{1}.json", PostsDirectory, entity.Id);
        }

    }
}
