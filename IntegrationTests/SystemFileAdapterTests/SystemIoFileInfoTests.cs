using System.IO;
using SystemFileAdapter;
using FluentAssertions;
using NUnit.Framework;

namespace SystemFileAdapterTests
{
    [TestFixture]
    public class SystemIoFileInfoTests
    {
        private string _fileName;

        [SetUp]
        public void Setup()
        {
            _fileName = "test";
            if(File.Exists(_fileName))
            {
                File.Delete(_fileName);
            }
        }

        [Test]
        public void GivenAFileName_WhenTheFileIsCreated_ThenTheSystemFIleInfoSaysFileExistsOnDisk()
        {
            var fileInfo = new SystemIoFileInfo(_fileName);
            Stream stream = null;
            try
            {
                stream = fileInfo.Create();
            }
            finally
            {
                if (stream != null) stream.Dispose();
            }
            fileInfo.Exists.Should().BeTrue();
        }

        [Test]
        public void GivenAFileName_WhenTheFileIsCreated_ThenTheFileExistsOnDisk()
        {
            var fileInfo = new SystemIoFileInfo(_fileName);
            Stream stream = null;
            try
            {
                stream = fileInfo.Create();
            }
            finally
            {
                if (stream != null) stream.Dispose();
            }
            File.Exists(_fileName).Should().BeTrue();
        }

        [Test]
        public void GivenAnExistingFile_WhenTheFileIsDeleted_ThenTheFileNoLongerExistsOnDisk()
        {
            var fileInfo = new SystemIoFileInfo(_fileName);
            Stream stream = null;
            try
            {
                stream = File.Create(_fileName);
            }
            finally
            {
                if (stream != null) stream.Dispose();
            }
            fileInfo.Delete();
            File.Exists(_fileName).Should().BeFalse();
        }

        [Test]
        public void GivenAnExistingFile_WhenTheFileIsOpened_ThenAStreamIsReturned()
        {
            var fileInfo = new SystemIoFileInfo(_fileName);
            Stream stream = null;
            try
            {
                stream = File.Create(_fileName);
            }
            finally
            {
                if (stream != null) stream.Dispose();
            }
            Stream openStream = null;
            try
            {
                openStream = fileInfo.Open(FileMode.Open);
            }
            finally
            {
                if (openStream != null) openStream.Dispose();
            }
            openStream.Should().NotBeNull();
        }

        [TearDown]
        public void TearDown()
        {
            if (File.Exists(_fileName))
            {
                File.Delete(_fileName);
            }
        }
    }
}
