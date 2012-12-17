using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ItsaRepository.interfaces;
using Moq;
using NUnit.Framework;
using Services;

namespace ServicesTests
{
    [TestFixture]
    class MediaServiceTests
    {
        private Mock<IMediaRepository> _postRepository;
        private MediaService _service;

        //void AddImage(Media media);
        public void GivenAValidImage_WhenItIsAddedToTheService_ThenItIsAddedToTheRepository()
        {
        }
    }
}
