using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using FluentAssertions;
using ItsaWeb.Models.Media;
using NUnit.Framework;

namespace ItsaWebTests.Models
{
    [TestFixture]
    class ShowMediaViewModelTests
    {
        [Test]
        public void GivenASizeAndAnAlignment_ThenTheCorrectClassStringIsReturned()
        {
            var model = new ShowMediaViewModel(new Media { Size = (int)Media.ValidSizes.Large, Alignment = (int)Media.ValidAllignments.Left });
            model.ClassString.Should().Contain("img-large");
            model.ClassString.Should().Contain("img-align-left");
        }

        [Test]
        public void GivenAFileNameWithAnExtension_ThenTheCorrectExtensionIsReturned()
        {
            var model = new ShowMediaViewModel(new Media { Size = (int)Media.ValidSizes.Large, Alignment = (int)Media.ValidAllignments.Left });
            model.FileName = "abc.foo.bar";
            model.Extension.Should().BeEquivalentTo("bar");
        }

        [Test]
        public void GivenAFileNameWithAnExtension_ThenTheAnErrorMessageIsReturned()
        {
            var model = new ShowMediaViewModel(new Media { Size = (int)Media.ValidSizes.Large, Alignment = (int)Media.ValidAllignments.Left });
            model.FileName = "abc";
            model.Extension.Should().BeEquivalentTo("Unknown File Type");
        }
    }
}
