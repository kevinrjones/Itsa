using System.Web.Mvc;
using Entities;
using FluentAssertions;
using ItsaWeb.Controllers;
using ItsaWeb.Models;
using Moq;
using NUnit.Framework;
using ServiceInterfaces;

namespace ItsaWebTests.Controllers
{
    [TestFixture]
    public class UserControllerTests
    {
        private Mock<IUserService> _userService;
        [SetUp]
        public void Setup()
        {
            _userService = new Mock<IUserService>();
        }

        [Test]
        public void GivenARegisteredUser_WhenITryAndReRegister_ThenIAmRedirectedToLogin()
        {
            _userService.Setup(u => u.GetUser()).Returns(new User());
            var controller = new UserController(_userService.Object, null, null);
            var view = controller.New(new RegisterUserViewModel());
            view.Should().BeOfType<RedirectToRouteResult>();
        }


        [Test]
        public void GivenAnUnRegisteredUser_WhenITryAndReRegister_ThenIAmShowTheRegisterView()
        {
            _userService.Setup(u => u.GetUser()).Returns((User) null);
            var controller = new UserController(_userService.Object, null, null);
            var view = controller.New(new RegisterUserViewModel());
            view.Should().BeOfType<ViewResult>();
        }
    }
}
