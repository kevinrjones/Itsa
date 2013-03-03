using System;
using System.Linq;
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
            _repository.Setup(r => r.Entities).Returns(new System.Collections.Generic.List<User> { new User{Name = ""} }.AsQueryable());

            var service = new UserService(_repository.Object);
            service.GetRegisteredUser().Should().BeNull();
        }

        [Test]
        public void GivenARegisteredUser_WhenAUserRegisters_ThenAnExceptionIsThrown()
        {
            _repository.Setup(r => r.Entities).Returns(new System.Collections.Generic.List<User> { new User { Email = "somename" } }.AsQueryable());

            var service = new UserService(_repository.Object);
            Action act = () => service.Register(It.IsAny<string>(), It.IsAny<string>());
            act.ShouldThrow<ItsaException>();
        }

        [Test]
        public void GivenAnUnRegisteredUser_WhenAUserRegisters_ThenCreateIsCalled()
        {
            _repository.Setup(r => r.Entities).Returns(new System.Collections.Generic.List<User> { new User { Email = "" } }.AsQueryable());

            var service = new UserService(_repository.Object);
            service.Register(It.IsAny<string>(), It.IsAny<string>());
            
            _repository.Verify(r => r.Create(It.IsAny<User>()), Times.Once());
        }

        [Test]
        public void GivenAnUnRegisteredUser_WhenAUserRegisters_ThenAnExceptionIsThrown()
        {
            _repository.Setup(r => r.Entities).Returns(new System.Collections.Generic.List<User> { new User { Email = "" } }.AsQueryable());

            var service = new UserService(_repository.Object);
            service.UnRegister().Should().BeFalse();            
        }

        [Test]
        public void GivenARegisteredUser_WhenAUserUnRegisters_ThenCreateIsCalled()
        {
            _repository.Setup(r => r.Entities).Returns(new System.Collections.Generic.List<User> { new User { Email = "somename" } }.AsQueryable());

            var service = new UserService(_repository.Object);
            service.UnRegister();

            _repository.Verify(r => r.Create(It.IsAny<User>()), Times.Once());
        }

        [Test]
        public void GivenAnUnregisteredUser_WhenTheUserLogon_ThenTheLogonThrowsAnException()
        {
            _repository.Setup(r => r.Entities).Returns(new System.Collections.Generic.List<User>().AsQueryable());
            var service = new UserService(_repository.Object);
            Action act = () =>service.Logon(It.IsAny<string>(), It.IsAny<string>());

            act.ShouldThrow<ItsaException>();
        }

        [Test]
        public void GivenARegisteredUser_WhenTheUserLogon_AndTheNameAndPassowrdMatch_ThenTheLogonSucceeds()
        {
            var user = new User("email", "password");
            _repository.Setup(r => r.Entities).Returns(new System.Collections.Generic.List<User> { user }.AsQueryable());
            var service = new UserService(_repository.Object);

            var loggedinUser =  service.Logon("email", "password");
            loggedinUser.Should().NotBeNull();
        }

        [Test]
        public void GivenARegisteredUser_WhenTheUserLogon_AndTheNameDoesNotMatch_ThenTheLogonFails()
        {
            var user = new User("email", "password");
            _repository.Setup(r => r.Entities).Returns(new System.Collections.Generic.List<User> { user }.AsQueryable());
            var service = new UserService(_repository.Object);
            Assert.Throws<ItsaException>(() => service.Logon("name1", "password"));
        }

        [Test]
        public void GivenARegisteredUser_WhenTheUserLogon_AndThePasswordDoesNotMatch_ThenTheLogonSucceeds()
        {
            var user = new User("email", "password");
            _repository.Setup(r => r.Entities).Returns(new System.Collections.Generic.List<User> { user }.AsQueryable());
            var service = new UserService(_repository.Object);

            Assert.Throws<ItsaException>(() => service.Logon("name1", "password"));
        }
    }
}
