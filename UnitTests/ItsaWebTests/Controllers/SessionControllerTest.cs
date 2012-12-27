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
        public void GivenARegisteredUser_WhenITryAndLogin_ThenIGetTheLoginPage()
        {
            _userService.Setup(u => u.GetRegisteredUser()).Returns(new User());
            var controller = new SessionController(_userService.Object, null, _logger.Object);
            var view = controller.New("redirect");
            view.Should().BeOfType<ViewResult>();
        }

        [Test]
        public void GivenAnUnRegisteredUser_WhenITryAndLogin_ThenTempDataContainsTheCorrectMessage()
        {
            _userService.Setup(u => u.GetRegisteredUser()).Returns((User) null);
            var controller = new SessionController(_userService.Object, null, _logger.Object);
            SetControllerContext(controller);
            controller.New("redirect");
            TempData["message"].Should().Be("User does not exist");
        }

        [Test]
        public void GivenAnUnRegisteredUser_WhenITryAndReRegister_ThenIAmShowTheRegisterView()
        {
            _userService.Setup(u => u.GetRegisteredUser()).Returns((User)null);
            var controller = new SessionController(_userService.Object, null, _logger.Object);
            var view = controller.New("redirect");
            view.Should().BeOfType<RedirectToRouteResult>();
            view.As<RedirectToRouteResult>().RouteValues["controller"].Should().Be("User");
            view.As<RedirectToRouteResult>().RouteValues["action"].Should().Be("New");
        }

        [Test]
        public void GivenAnInvalidUser_WhenTheSessionIsCreated_ThenTheUserIsRegistered()
        {
            const string userName = "name";
            const string password = "password";
            
            var controller = new SessionController(_userService.Object, _configurationManager.Object, _logger.Object);

            SetControllerContext(controller);

            controller.Create(new LogonViewModel { UserName = userName, Password = password });
            _userService.Verify(u => u.Logon(userName, password), Times.Once());
        }

        [Test]
        public void GivenAnInvalidUser_WhenTheSessionIsCreated_ThenIndexViewIsReturned()
        {
            const string userName = "name";
            const string password = "password";
            
            var controller = new SessionController(_userService.Object, _configurationManager.Object, _logger.Object);

            SetControllerContext(controller);

            var view = controller.Create(new LogonViewModel { UserName = userName, Password = password });
            view.Should().BeOfType<ViewResult>();
        }

        [Test]
        public void GivenAValidUser_WhenTheSessionIsCreated_AndThereIsNoRedirect_ThenTheDefaultRedirectViewIsReturned()
        {
            const string userName = "name";
            const string password = "password";
            _userService.Setup(u => u.Logon(userName, password)).Returns(true);
            var controller = new SessionController(_userService.Object, _configurationManager.Object, _logger.Object);

            SetControllerContext(controller);

            var view = controller.Create(new LogonViewModel { UserName = userName, Password = password });
            view.Should().BeOfType<RedirectToRouteResult>();
            view.As<RedirectToRouteResult>().RouteValues["controller"].Should().Be("Itsa");
            view.As<RedirectToRouteResult>().RouteValues["action"].Should().Be("Index");
        }

        [Test]
        public void GivenAValidUser_WhenTheSessionIsCreated_AndThereIsARedirect_ThenTheDefaultRedirectViewIsReturned()
        {
            const string userName = "name";
            const string password = "password";
            const string redirect = "Itsa/Foo";
            _userService.Setup(u => u.Logon(userName, password)).Returns(true);
            var controller = new SessionController(_userService.Object, _configurationManager.Object, _logger.Object);

            SetControllerContext(controller);

            var view = controller.Create(new LogonViewModel { UserName = userName, Password = password, RedirectTo = redirect});
            view.Should().BeOfType<RedirectResult>();
            view.As<RedirectResult>().Url.Should().Be("Itsa/Foo");
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