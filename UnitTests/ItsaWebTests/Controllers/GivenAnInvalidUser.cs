using System.Web.Mvc;
using AbstractConfigurationManager;
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
        public class GivenAnInvalidUser : ControllerTestsBase
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
            public void WhenTheSessionIsCreated_ThenTheUserIsRegistered()
            {
                const string userName = "name";
                const string password = "password";

                var controller = new SessionController(_userService.Object, _configurationManager.Object, _logger.Object);

                SetControllerContext(controller);

                controller.Create(new LogonViewModel { Email = userName, Password = password });
                _userService.Verify(u => u.Logon(userName, password), Times.Once());
            }

            [Test]
            public void WhenTheSessionIsCreated_ThenTheUserIsNotAuthenticated()
            {
                const string userName = "name";
                const string password = "password";

                var controller = new SessionController(_userService.Object, _configurationManager.Object, _logger.Object);

                SetControllerContext(controller);

                var view = (JsonResult)controller.Create(new LogonViewModel { Email = userName, Password = password });
                var model = (UserViewModel)view.Data;
                model.IsAuthenticated.Should().BeFalse();
            }
        }
    }
}