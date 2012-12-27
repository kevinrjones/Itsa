using System.Web.Mvc;
using AbstractConfigurationManager;
using Entities;
using FluentAssertions;
using ItsaWeb.Controllers;
using ItsaWeb.Models;
using ItsaWeb.Models.Users;
using Logging;
using Moq;
using NUnit.Framework;
using ServiceInterfaces;

namespace ItsaWebTests.Controllers
{
    [TestFixture]
    public class UserControllerTests : ControllerTestsBase
    {
        private Mock<IUserService> _userService;
        private Mock<IConfigurationManager> _configurationManager;
        private Mock<ILogger> _logger;

        [SetUp]
        public void Setup()
        {
            _userService = new Mock<IUserService>();
            _configurationManager = new Mock<IConfigurationManager>();
            _logger = new Mock<ILogger>();

            _configurationManager.Setup(c => c.AppSetting("cookie")).Returns("anything");
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
        public void GivenARegisteredUser_WhenITryAndReRegister_ThenTempDataContainsTheCorrectMessage()
        {
            _userService.Setup(u => u.GetRegisteredUser()).Returns(new User());
            var controller = new UserController(_userService.Object, null, _logger.Object);
            SetControllerContext(controller);
            controller.New(new RegisterUserViewModel());
            TempData["message"].Should().Be("User is already registered");
        }

        [Test]
        public void GivenAnUnRegisteredUser_WhenITryAndReRegister_ThenIAmShowTheRegisterView()
        {
            _userService.Setup(u => u.GetRegisteredUser()).Returns((User) null);
            var controller = new UserController(_userService.Object, null, _logger.Object);
            var view = controller.New(new RegisterUserViewModel{UserName = "user"});
            view.Should().BeOfType<ViewResult>();
        }

        [Test]
        public void GivenAValidUser_WhenTheUserIsCreated_ThenTheUserIsRegistered()
        {
            const string userName = "name";
            const string password = "password";
            const string email = "email";
            var controller = new UserController(_userService.Object, _configurationManager.Object, _logger.Object);

            SetControllerContext(controller);

            controller.Create(new RegisterUserViewModel{UserName = userName, Email = email, Password = password});
            _userService.Verify(u => u.Register(userName, password, email), Times.Once());
        }

        [Test]
        public void GivenAValidUser_WhenTheUserIsCreated_ThenIndexViewIsReturned()
        {
            const string userName = "name";
            const string password = "password";
            const string email = "email";
            var controller = new UserController(_userService.Object, _configurationManager.Object, _logger.Object);

            SetControllerContext(controller);

            var view = controller.Create(new RegisterUserViewModel { UserName = userName, Email = email, Password = password });
            view.Should().BeOfType<RedirectToRouteResult>();
            view.As<RedirectToRouteResult>().RouteValues["controller"].Should().Be("Itsa");
            view.As<RedirectToRouteResult>().RouteValues["action"].Should().Be("Index");
        }

        [Test]
        public void GivenARegisteredUser_WhenITryAndDelete_ThenTempDataContainsTheCorrectMessage()
        {
            _userService.Setup(u => u.UnRegister()).Returns(true);
            var controller = new UserController(_userService.Object, null, _logger.Object);
            SetControllerContext(controller);
            controller.Delete();
            TempData["message"].Should().Be("User unregistered");
        }

        [Test]
        public void GivenAnUnregisteredUser_WhenITryAndDelete_ThenTempDataContainsTheCorrectMessage()
        {
            _userService.Setup(u => u.UnRegister()).Returns(false);
            var controller = new UserController(_userService.Object, null, _logger.Object);
            SetControllerContext(controller);
            controller.Delete();
            TempData["message"].Should().Be("User already unregistered");
        }

        [Test]
        public void WhenAUserIsDeleted_ThenTheControllerRedirectsToTheNewView()
        {
            _userService.Setup(u => u.UnRegister()).Returns(false);
            var controller = new UserController(_userService.Object, null, _logger.Object);
            SetControllerContext(controller);
            var view = controller.Delete();
            view.Should().BeOfType<RedirectToRouteResult>();
            view.As<RedirectToRouteResult>().RouteValues["action"].Should().Be("New");
        }
    }
}
