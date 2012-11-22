using System;
using Entities;
using Exceptions;
using FluentAssertions;
using ItsaRepository.Interfaces;
using Moq;
using NUnit.Framework;
using Services;

namespace ServicesTests
{
    [TestFixture]
    class UserServiceTests
    {
        Mock<IUserRepository> _repository = new Mock<IUserRepository>();

        [SetUp]
        public void Setup()
        {
            _repository = new Mock<IUserRepository>();
        }

        [Test]
        public void GivenNoUser_WhenAUserIsReturned_ThenNullIsReturned()
        {
            _repository.Setup(r => r.GetUser()).Returns((User) new User{Name = ""});

            var service = new UserService(_repository.Object);
            service.GetRegisteredUser().Should().BeNull();
        }

        [Test]
        public void GivenARegisteredUser_WhenAUserRegisters_ThenAnExceptionIsThrown()
        {
            _repository.Setup(r => r.GetUser()).Returns(new User { Name = "somename" });

            var service = new UserService(_repository.Object);
            Action act = () => service.Register(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>());
            act.ShouldThrow<ItsaException>();
        }

        [Test]
        public void GivenAnUnRegisteredUser_WhenAUserRegisters_ThenCreateIsCalled()
        {
            _repository.Setup(r => r.GetUser()).Returns(new User { Name = "" });

            var service = new UserService(_repository.Object);
            service.Register(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>());
            
            _repository.Verify(r => r.Create(It.IsAny<User>()), Times.Once());
        }

        [Test]
        public void GivenAnUnRegisteredUser_WhenAUserRegisters_ThenAnExceptionIsThrown()
        {
            _repository.Setup(r => r.GetUser()).Returns(new User { Name = "" });

            var service = new UserService(_repository.Object);
            Action act = () => service.UnRegister();
            act.ShouldThrow<ItsaException>();
        }

        [Test]
        public void GivenARegisteredUser_WhenAUserUnRegisters_ThenCreateIsCalled()
        {
            _repository.Setup(r => r.GetUser()).Returns(new User { Name = "somename" });

            var service = new UserService(_repository.Object);
            service.UnRegister();

            _repository.Verify(r => r.Create(It.IsAny<User>()), Times.Once());
        }


    }
}
