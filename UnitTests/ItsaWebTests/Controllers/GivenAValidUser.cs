using System;
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
    namespace SessionControllerTest
    {
        [TestFixture]
        internal class GivenAValidUser : ControllerTestsBase
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
            public void WhenTheSessionIsCreated_TheTheUserShouldBeAuthenticated()
            {
                const string userName = "name";
                const string password = "password";
                _userService.Setup(u => u.Logon(userName, password)).Returns(new User());
                var controller = new SessionController(_userService.Object, _configurationManager.Object, _logger.Object);

                SetControllerContext(controller);

                var view = (JsonResult)controller.Create(new LogonViewModel { Email = userName, Password = password });
                var model = (UserViewModel)view.Data;
                model.IsAuthenticated.Should().BeTrue();
            }

            [Test]
            public void WhenTheSessionIsDestroyed_TheTheUserShouldNotBeAuthenticated()
            {
                const string userName = "name";
                const string password = "password";
                _userService.Setup(u => u.Logon(userName, password)).Returns(new User());
                var controller = new SessionController(_userService.Object, _configurationManager.Object, _logger.Object);

                SetControllerContext(controller);

                var view = controller.Delete();
                var model = (UserViewModel)view.Data;
                model.IsAuthenticated.Should().BeFalse();
            }

            [Test]
            public void WhenTheSessionIsDestroyed_TheTheCookieShouldBeReset()
            {
                const string userName = "name";
                const string password = "password";
                _userService.Setup(u => u.Logon(userName, password)).Returns(new User());
                var controller = new SessionController(_userService.Object, _configurationManager.Object, _logger.Object);

                SetControllerContext(controller);

                var view = controller.Delete();
                var cookie = FakeResponse.Cookies.Get(0);
                cookie.Expires.Should().BeBefore(DateTime.Now);
            }
        }
    }
}