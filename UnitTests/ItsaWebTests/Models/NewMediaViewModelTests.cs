using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mime;
using System.Web;
using FluentAssertions;
using ItsaWeb.Models.Media;
using NUnit.Framework;

namespace ItsaWebTests.Models
{
    [TestFixture]
    internal class NewMediaViewModelTests
    {
        [Test]
        public void GivenAFile_TheCorrectContentTypeIsReturned()
        {
            var model = new NewMediaViewModel();
            var file = new TestableHttpPostedFileBase();
            model.File = file;
            model.ContentType.Should().Be("test/foo");
        }

        [Test]
        public void GivenAQqFile_TheCorrectContentTypeIsReturned()
        {
            var model = new NewMediaViewModel();
            model.QqFile = "foo.jpg";
            model.ContentType.Should().Be("image/jpeg");
        }

        [Test]
        public void GivenAQqFile_AndAnInvalidExtension_TheAnExceptionIsThrown()
        {
            var model = new NewMediaViewModel();
            model.QqFile = "foo.foo";
            model.ContentType.Should().BeEmpty();
        }

        [Test]
        public void GivenAQqFile_AndAnNoExtension_TheAnExceptionIsThrown()
        {
            var model = new NewMediaViewModel();
            model.QqFile = "foo";
            model.ContentType.Should().BeEmpty();
        }

        [Test]
        public void WhenTheModelIsValid_ThenNoMessageIsOutput()
        {
            var model = new NewMediaViewModel();
            var file = new TestableHttpPostedFileBase();
            model.File = file;
            var context = new ValidationContext(new object());
            var results = model.Validate(context);
            results.Count().Should().Be(0);
        }

        [Test]
        public void WhenTheModelIsInValid_ThenNoMessageIsOutput()
        {
            var model = new NewMediaViewModel();
            var file = new TestableHttpPostedFileBase();
            file.GivenFileName = "test.bar";
            model.File = file;
            var context = new ValidationContext(new object());
            var results = model.Validate(context);
            results.Count().Should().Be(1);
        }

        [Test]
        public void WhenTheModelHasAnInvalidQqFile_ThenNoMessageIsOutput()
        {
            var model = new NewMediaViewModel();
            model.QqFile = "test.bar";
            var context = new ValidationContext(new object());
            var results = model.Validate(context);
            results.Count().Should().Be(1);
        }
    }

    internal class TestableHttpPostedFileBase : HttpPostedFileBase
    {
        public string GivenFileName = "foo.jpg";
        public override string ContentType
        {
            get
            {
                return "test/foo";
            }
        }

        public override string FileName
        {
            get { return GivenFileName; }
        }
    }
}