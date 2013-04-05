using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Entities;
using Exceptions;
using FluentAssertions;
using ItsaRepository.interfaces;
using Moq;
using NUnit.Framework;
using Services;

namespace ServicesTests
{
    [TestFixture]
    public class MediaServiceTest
    {
        [SetUp]
        public void Setup()
        {
            _mediaRepository = new Mock<IMediaRepository>();
            _mediaService = new MediaService(_mediaRepository.Object);
        }

        private Mock<IMediaRepository> _mediaRepository;
        private MediaService _mediaService;
        private const string Filename = "foo.jpg";

        [Test]
        public void
            GivenARequestForMedia_AndThereAreMoreMediaInTheStoreThanRequested_ThenOnlyTheCorrectNumberOfMediaAreReturned
            ()
        {
            _mediaRepository.Setup(m => m.Entities).Returns(new List<Media> { new Media(), new Media() }.AsQueryable());
            IEnumerable<Media> media = _mediaService.GetMedia(1, 1);
            media.Count().Should().Be(1);
        }

        [Test]
        public void
            GivenARequestForMedia_AndThereAreMoreMediaInTheStoreThanRequested_ThenOnlyTheRequestedMediaAreReturned()
        {
            _mediaRepository.Setup(m => m.Entities)
                            .Returns(
                                new List<Media>
                                    {
                                        new Media {Year = 2001},
                                        new Media {Year = 2002},
                                        new Media {Year = 2003}
                                    }.AsQueryable());
            var media = _mediaService.GetMedia(1, 2).ToList();
            media[0].Year.Should().Be(2001);
            media[1].Year.Should().Be(2002);
        }

        [Test]
        public void GivenAnExistingMedia_WhenDuplicateMediaIsCreated_ThenAnExceptionIsThrown()
        {
            _mediaRepository.Setup(
                m => m.Entities).Returns(new List<Media> { new Media { FileName = Filename } }.AsQueryable());

            var stream = new Mock<Stream>();
            Assert.Throws<ItsaException>(
                () =>
                _mediaService.CreateMedia(Filename, It.IsAny<string>(), stream.Object, It.IsAny<int>()));
        }

        [Test]
        public void
            WhenExistingMediaIsRequested_AndTheDataBaseIsNotAvailable_ThenAnMBlogExceptionIsThrown()
        {
            _mediaRepository.Setup(m => m.Entities).Throws<Exception>();
            Assert.Throws<ItsaException>(() => _mediaService.GetMedia(It.IsAny<int>(), It.IsAny<int>()));
        }

        [Test]
        public void WhenExistingMediaIsRequested_ThenTheMediaIsReturned()
        {
            DateTime now = DateTime.Now;
            _mediaRepository.Setup(
                m => m.Entities).Returns(new List<Media>
                    {
                        new Media {FileName = Filename, Year = now.Year, Month = now.Month, Day = now.Day, LinkKey = ""}
                    }.AsQueryable());
            Media media = _mediaService.GetMedia(now.Year, now.Month, now.Day, "");
            Assert.That(media, Is.Not.Null);
        }

        [Test]
        public void WhenMediaIsCreated_AndTheDataBaseIsNotAvailable_ThenAnMBlogExceptionIsThrown()
        {
            _mediaRepository.Setup(m => m.Entities).Throws<Exception>();
            var stream = new Mock<Stream>();

            Assert.Throws<ItsaException>(
                () =>
                _mediaService.CreateMedia(Filename, It.IsAny<string>(), stream.Object, It.IsAny<int>()));
        }

        [Test]
        public void WhenMediaIsCreated_ThenTheUrlToTheMediaIsReturned()
        {
            DateTime now = DateTime.Now;
            string linkKey = Filename.Split('.').First();
            _mediaRepository.Setup(m => m.Entities).Returns(new List<Media>
                {
                    new Media {FileName = Filename, Year = now.Year, Month = now.Month, Day = now.Day, LinkKey = ""}
                }.AsQueryable());
            _mediaRepository.Setup(m => m.Create(It.IsAny<Media>())).Returns(new Media { LinkKey = linkKey });
            var stream = new Mock<Stream>();

            string url = _mediaService.CreateMedia("otherfile", It.IsAny<string>(), stream.Object,
                                                   It.IsAny<int>()).Url;
            Assert.That(url, Is.EqualTo(string.Format("{0}/{1}/{2}/{3}", now.Year, now.Month, now.Day, linkKey)));
        }

        [Test]
        public void WhenMediaIsCreated_ThenTheIdToTheMediaIsReturned()
        {
            DateTime now = DateTime.Now;
            var guid = Guid.NewGuid();
            _mediaRepository.Setup(m => m.Entities)
                            .Returns(
                                new List<Media>
                                    {
                                        new Media
                                            {
                                                FileName = Filename,
                                                Year = now.Year,
                                                Month = now.Month,
                                                Day = now.Day,
                                                LinkKey = ""
                                            }
                                    }.AsQueryable());
            _mediaRepository.Setup(m => m.Create(It.IsAny<Media>())).Returns(new Media { Id = guid });
            var stream = new Mock<Stream>();

            Guid id = _mediaService.CreateMedia("otherfile", It.IsAny<string>(), stream.Object, It.IsAny<int>()).Id;
            Assert.That(id, Is.EqualTo(guid));
        }

        [Test]
        public void WhenMediaIsUpdated_AndTheMediaCannotBeFound_ThenAnExcpetionIsThrown()
        {
            var guid = Guid.NewGuid();
            _mediaRepository.Setup(m => m.Entities).Returns(new List<Media> { }.AsQueryable());
            Action act =
                () =>
                _mediaService.UpdateMediaDetails(guid, "", It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>());
            act.ShouldThrow<ItsaException>();
        }

        [Test]
        public void WhenMediaIsUpdated_AndTheRepositoryThrowsAnException_ThenAnExcpetionIsThrown()
        {
            var guid = Guid.NewGuid();
            _mediaRepository.Setup(m => m.Entities).Returns(new List<Media> { new Media { Id = guid } }.AsQueryable());
            _mediaRepository.Setup(m => m.Update(It.IsAny<Media>())).Throws<Exception>();
            Action act =
                () =>
                _mediaService.UpdateMediaDetails(guid, "", It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>());
            act.ShouldThrow<ItsaException>();
        }

        [Test]
        public void WhenMediaIsUpdated_ThenItIsUpdated()
        {
            var guid = Guid.NewGuid();
            _mediaRepository.Setup(m => m.Entities).Returns(new List<Media> { new Media { Id = guid } }.AsQueryable());
            _mediaService.UpdateMediaDetails(guid, "", It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>());
            Assert.True(true, "No exceptions thrown");
        }

        [Test]
        public void WhenMediaIsUpdated_ThenTheUpdatesAreWrittenToTheRepository()
        {
            DateTime now = DateTime.Now;
            _mediaRepository.Setup(
                m => m.Entities)
                            .Returns(
                                new List<Media>
                                    {
                                        new Media {FileName = Filename, Year = now.Year, Month = now.Month, Day = now.Day}
                                    }
                                    .AsQueryable());
            var stream = new Mock<Stream>();

            var media = new Media
                {
                    FileName = Filename,
                    Title = "title",
                    Caption = "caption",
                    Description = "description",
                    Alternate = "alternate",
                    MimeType = "contenttype",
                    Alignment = (int)Media.ValidAllignments.Left,
                    Size = (int)Media.ValidSizes.Large,
                    Data = new byte[] { 0, 0 }
                };

            _mediaService.UpdateMedia(Filename, "title", "caption", "description", "alternate", "contenttype",
                                      (int)Media.ValidAllignments.Left, (int)Media.ValidSizes.Large, stream.Object, 2);

            _mediaRepository.Verify(m => m.Update(media));
        }

        [Test]
        public void WhenMediaIsUpdated_AndTheDatabaseThrowsAnException_ThenAnExceptionIsThrown()
        {
            _mediaRepository.Setup(m => m.Update(It.IsAny<Media>())).Throws<Exception>();
            var stream = new Mock<Stream>();

            Action act =
                () =>
                _mediaService.UpdateMedia(Filename, It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                                          It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(),
                                          stream.Object, It.IsAny<int>());

            act.ShouldThrow<ItsaException>();
        }

        [Test]
        public void WhenNonExistingMediaIsRequested_ThenNullIsreturned()
        {
            _mediaRepository.Setup(m => m.Entities).Returns((new List<Media>().AsQueryable()));

            var media = _mediaService.GetMedia(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>());
            media.Should().BeNull();
        }

        [Test]
        public void WhenExistingMediaIsRequestedViaAnId_ThenItIsreturned()
        {
            var guid = Guid.NewGuid();
            _mediaRepository.Setup(m => m.Entities).Returns((new List<Media> { new Media { Id = guid } }.AsQueryable()));

            var media = _mediaService.GetMedia(guid);
            media.Should().NotBeNull();
        }

        [Test]
        public void WhenNonExistingMediaIsRequestedViaAnId_ThenNullIsreturned()
        {
            _mediaRepository.Setup(m => m.Entities).Returns((new List<Media>().AsQueryable()));

            var media = _mediaService.GetMedia(It.IsAny<Guid>());
            media.Should().BeNull();
        }

        [Test]
        public void WhenMediaIsDeleted_ThenItIsDeleted()
        {
            _mediaRepository.Setup(m => m.Entities).Returns(new List<Media> { new Media() }.AsQueryable());
            _mediaService.DeleteMedia(It.IsAny<Guid>());
            Assert.True(true, "No exceptions thrown");
        }

        [Test]
        public void WhenMediaIsDeleted_AndTheRepositoryThrowsAnException_ThenAnExceptionIsThrown()
        {
            _mediaRepository.Setup(m => m.Entities).Returns(new List<Media> { new Media() }.AsQueryable());
            _mediaRepository.Setup(m => m.Delete(It.IsAny<Media>())).Throws<Exception>();
            Action act = () => _mediaService.DeleteMedia(It.IsAny<Guid>());
            act.ShouldThrow<ItsaException>();
        }

    }
}