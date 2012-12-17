using System;
using System.IO;
using Entities;
using FluentAssertions;
using NUnit.Framework;

namespace ModelTests
{
    [TestFixture]
    internal class MediaTest
    {
        [Test]
        public void GivenAnImageObject_ThenItIsHashCodeIsEqualToItsId()
        {
            var id = new Guid();
            var img = new Media {Id= id};

            Assert.That(img.GetHashCode(), Is.EqualTo(id.GetHashCode()));
        }

        [Test]
        public void GivenAnImageObject_WhenItIsComparedToAnotherType_ThenTheyAreNotEqual()
        {
            var img = new Media();
            bool actual = img.Equals("string");
            Assert.That(actual, Is.False);
        }

        [Test]
        public void GivenAnImageObject_WhenItIsComparedToItself_AndItIsCastToObject_ThenTheyAreEqual()
        {
            var img = new Media();
            bool actual = img.Equals((object) img);
            Assert.That(actual, Is.True);
        }

        [Test]
        public void GivenAnImageObject_WhenItIsComparedToItself_ThenTheyAreEqual()
        {
            var img = new Media();
            bool actual = img.Equals(img);
            Assert.That(actual, Is.True);
        }

        [Test]
        public void GivenAnImageObject_WhenItIsComparedToAnotherMedia_AndTheIdsAreEqual_ThenTheyAreEqual()
        {
            var id = Guid.NewGuid();
            var img1 = new Media { Id = id };
            var img2 = new Media { Id = id };
            img1.Equals(img2).Should().BeTrue();
        }

        [Test]
        public void GivenAnImageObject_WhenItIsComparedToAnotherMedia_AndItIsCastToObject_AndTheIdsAreEqual_ThenTheyAreEqual()
        {
            var id = Guid.NewGuid();
            var img1 = new Media { Id = id };
            var img2 = new Media { Id = id };
            img1.Equals((object)img2).Should().BeTrue();
        }

        [Test]
        public void GivenAnInvalidSize_WhenICreateAnImage_ThenTheSizeIsSetToFullsize()
        {
            var media = new Media("filename", "title", "caption", "description", "alternate", "mime",
                                  (int) Media.ValidAllignments.None, 43, new byte[] {});
            Assert.That(media.Size, Is.EqualTo((int) Media.ValidSizes.Fullsize));
        }

        [Test]
        public void GivenImageData_WhenICreateAnImage_ThenIGetValidDates()
        {
            DateTime today = DateTime.Now;
            var media = new Media("filename", "title", "caption", "description", "alternate", "mime",
                                  (int) Media.ValidAllignments.None, (int) Media.ValidSizes.Fullsize, new byte[] {});
            Assert.That(media.Year, Is.EqualTo(today.Year));
            Assert.That(media.Month, Is.EqualTo(today.Month));
            Assert.That(media.Day, Is.EqualTo(today.Day));
        }

        [Test]
        public void GivenTwoImageObjects_WhenOneIsNull_AndItIsCastToObject_ThenTheyAreNotEqual()
        {
            var img = new Media();
            bool actual = img.Equals((object) null);
            Assert.That(actual, Is.False);
        }

        [Test]
        public void GivenTwoImageObjects_WhenOneIsNull_ThenTheyAreNotEqual()
        {
            var img = new Media();
            bool actual = img.Equals(null);
            Assert.That(actual, Is.False);
        }

        [Test]
        public void WhenAUrlIsRequested_TheCorrectlyFormattedUrlIsReturned()
        {
            var media = new Media {Year = 2010, Month = 1, Day = 2, LinkKey = "link"};

            Assert.That(media.Url, Is.EqualTo("2010/1/2/link"));
        }

        [Test]
        public void WhenAnInvalidAlignmentIsSet_ThenTheAlignmentIsSetToNone()
        {
            var media = new Media();
            media.Alignment = 23;

            Assert.That(media.Alignment, Is.EqualTo(0));
        }

        [Test]
        public void WhenCreatingAMediaWithAShortConstructor_ThenTheInputStreamCannotBeNull()
        {
            Action a = () => new Media("", "", null, 0);
            a.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void WhenCreatingAMediaWithAShortConstructor_ThenTheDefaulsValuesAreSet()
        {
            var m = new Media("", "", new MemoryStream(), 0);
            m.Alignment.Should().Be(0);
            m.Title.Should().Be("");
            m.Caption.Should().Be("");
            m.Description.Should().Be("");
            m.Alternate.Should().Be("");
            m.Size.Should().Be(0);
            m.Data.Should().NotBeNull();
        }

        [Test]
        public void WhenGettingATitle_ThenTheTitleIsReturned()
        {
            var m = new Media();
            m.Title = "title";
            m.Title.Should().Be("title");
        }

        [Test]
        public void GivenAnInvalidAlignmenmt_ThenAnAlignmentOfNoneIsReturned()
        {
            var m = new Media();
            m.Alignment = 43;
            m.Alignment.Should().Be((int)Media.ValidAllignments.None);
        }
    }
}