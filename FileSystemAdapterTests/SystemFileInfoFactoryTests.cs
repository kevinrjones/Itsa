using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemFileAdapter;
using FluentAssertions;
using NUnit.Framework;

namespace FileSystemAdapterTests
{
    [TestFixture]
    public class SystemFileInfoFactoryTests
    {
        [Test]
        public void GivenASystemFileInfoFactory_WhenASystemFIleInfoIsCreated_ThenASystemFIleInfoIsReturned()
        {
            var factory = new SystemFileInfoFactory();
            var fileinfo = factory.CreateFileInfo("filename");
            fileinfo.Should().NotBeNull();
        }
    }
}
