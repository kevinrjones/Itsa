using System.Web.Mvc;
using AbstractConfigurationManager;
using Entities;
using FluentAssertions;
using ItsaWeb.Controllers;
using ItsaWeb.Models.Users;
using Logging;
using Moq;
using NUnit.Framework;
using ServiceInterfaces;

namespace ItsaWebTests.Controllers
{
    [TestFixture]
    class SessionControllerTest : ControllerTestsBase
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
        public void GivenAnInvalidUser_WhenTheSessionIsCreated_ThenTheUserIsRegistered()
        {
            const string userName = "name";
            const string password = "password";
            
            var controller = new SessionController(_userService.Object, _configurationManager.Object, _logger.Object);

            SetControllerContext(controller);

            controller.Create(new LogonViewModel { Email = userName, Password = password });
            _userService.Verify(u => u.Logon(userName, password), Times.Once());
        }

        [Test]
        public void GivenAnInvalidUser_WhenTheSessionIsCreated_ThenTheUserIsNotAuthenticated()
        {
            const string userName = "name";
            const string password = "password";
            
            var controller = new SessionController(_userService.Object, _configurationManager.Object, _logger.Object);

            SetControllerContext(controller);

            var view = (JsonResult) controller.Create(new LogonViewModel { Email = userName, Password = password });
            var model = (UserViewModel) view.Data;
            model.IsAuthenticated.Should().BeFalse();
        }

        [Test]
        public void GivenAValidUser_WhenTheSessionIsCreated_TheTheUserShouldBeAuthenticated()
        {
            const string userName = "name";
            const string password = "password";
            _userService.Setup(u => u.Logon(userName, password)).Returns(new User());
            var controller = new SessionController(_userService.Object, _configurationManager.Object, _logger.Object);

            SetControllerContext(controller);

            var view = (JsonResult) controller.Create(new LogonViewModel { Email = userName, Password = password });
            var model = (UserViewModel)view.Data;
            model.IsAuthenticated.Should().BeTrue();
        }

        [Test]
        public void GivenARegisteredUser_WhenITryAndLogout_ThenTempDataContainsTheCorrectMessage()
        {
            _userService.Setup(u => u.UnRegister()).Returns(true);
            var controller = new SessionController(_userService.Object, _configurationManager.Object, _logger.Object);
            SetControllerContext(controller);
            controller.Delete();
            TempData["message"].Should().Be("Session ended");
        }

        [Test]
        public void WhenAUserIsDeleted_ThenTheControllerRedirectsToTheNewView()
        {
            _userService.Setup(u => u.UnRegister()).Returns(false);
            var controller = new SessionController(_userService.Object, _configurationManager.Object, _logger.Object);
            SetControllerContext(controller);
            var view = controller.Delete();
            view.Should().BeOfType<RedirectToRouteResult>();
            view.As<RedirectToRouteResult>().RouteValues["action"].Should().Be("New");
        }
    }
}