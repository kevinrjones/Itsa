using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemFileAdapter;
using Entities;
using FileRepository.Repositories;
using FileSystemInterfaces;
using FluentAssertions;
using NUnit.Framework;

namespace FileRepositoryTests
{
    [TestFixture]
    public class MediaRepositoryTests
    {
        [Test]
        public void GivenAMediaRepository_ThenItsFilenameShouldContainTheStringMedia()
        {
            var repository = new MyMediaRepository("foo", null, null);
            var fileName = repository.GeneratedFileName(new Media {Id = new Guid()});
            fileName.Should().Contain("media");
        }
    }

    public class MyMediaRepository : MediaRepository
    {
        public MyMediaRepository(string path, IFileInfoFactory fileInfo, IDirectoryInfo directoryInfo) : base(path, fileInfo, directoryInfo)
        {
        }

        public string GeneratedFileName(Media e)
        {
            return GenerateFileName(e);
        }
    }
}
