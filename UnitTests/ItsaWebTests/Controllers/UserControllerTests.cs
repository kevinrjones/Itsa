using System.Web.Mvc;
using Entities;
using FluentAssertions;
using ItsaWeb.Controllers;
using ItsaWeb.Models;
using Logging;
using Moq;
using NUnit.Framework;
using ServiceInterfaces;

namespace ItsaWebTests.Controllers
{
    [TestFixture]
    public class UserControllerTests
    {
        private Mock<IUserService> _userService;
        private Mock<ILogger> _logger;

        [SetUp]
        public void Setup()
        {
            _userService = new Mock<IUserService>();
            _logger = new Mock<ILogger>();
        }

        [Test]
        public void GivenARegisteredUser_WhenITryAndReRegister_ThenIAmRedirectedToLogin()
        {
            _userService.Setup(u => u.GetRegisteredUser()).Returns(new User());
            var controller = new UserController(_userService.Object, null, _logger.Object);
            var view = controller.New(new RegisterUserViewModel());
            view.Should().BeOfType<RedirectToRouteResult>();
        }


        [Test]
        public void GivenAnUnRegisteredUser_WhenITryAndReRegister_ThenIAmShowTheRegisterView()
        {
            _userService.Setup(u => u.GetRegisteredUser()).Returns((User) null);
            var controller = new UserController(_userService.Object, null, _logger.Object);
            var view = controller.New(new RegisterUserViewModel());
            view.Should().BeOfType<ViewResult>();
        }
    }
}
