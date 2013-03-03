//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Web.Mvc;
//using Entities;
//using FluentAssertions;
//using ItsaWeb.Controllers;
//using ItsaWeb.Models;
//using ItsaWeb.Models.Users;
//using Moq;
//using NUnit.Framework;
//using ServiceInterfaces;

//namespace ItsaWebTests.Controllers
//{
//    [TestFixture]
//    class AdminControllerTests
//    {
//        readonly Mock<IUserService> _userService = new Mock<IUserService>();

//        [SetUp]
//        public void Setup()
//        {
//            _userService.Setup(u => u.GetRegisteredUser()).Returns(new User {Name = "Kevin"});
//        }

//        [Test]
//        public void WhenTheIndexPageIsAccessed_ThenTheIndexViewResultIsReturned()
//        {
//            var controller = new AdminController(_userService.Object);
//            var result = controller.Index();
//            result.Should().BeOfType<ViewResult>();
//        }

//        [Test]
//        public void WhenTheIndexPageIsAccessed_ThenTheIndexViewShouldContainTheModel()
//        {
//            var controller = new AdminController(_userService.Object);
//            var result = (ViewResult)controller.Index();
//            result.Model.Should().BeOfType<UserViewModel>();
//        }

//        [Test]
//        public void WhenTheIndexPageContainsTheModel_ThenTheModelShouldHaveTheUsersName()
//        {
//            var controller = new AdminController(_userService.Object);
//            var result = (ViewResult)controller.Index();
//            result.Model.As<UserViewModel>().Name.Should().Be("Kevin");
//        }

//        [Test]
//        public void GivenNoUser_WhenTheIndexIsAccess_ThenTheUserIsRedirectedToTheNewUserPage()
//        {
//            _userService.Setup(u => u.GetRegisteredUser()).Returns((User) null);
//            var controller = new AdminController(_userService.Object);
//            var result = controller.Index();
//            result.Should().BeOfType<RedirectToRouteResult>();
//        }

//        [Test]
//        public void GivenNoUser_WhenTheIndexIsAccess_ThenTheRedirectRouteIsUserNew()
//        {
//            _userService.Setup(u => u.GetRegisteredUser()).Returns((User)null);
//            var controller = new AdminController(_userService.Object);
//            var result = controller.Index();
//            result.As<RedirectToRouteResult>().RouteValues["controller"].Should().Be("User");
//            result.As<RedirectToRouteResult>().RouteValues["action"].Should().Be("New");
//        }
//    }
//}
