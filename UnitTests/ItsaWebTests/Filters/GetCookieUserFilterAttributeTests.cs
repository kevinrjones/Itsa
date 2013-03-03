using System;
using System.Security.Principal;
using System.Text;
using System.Web;
using System.Web.Mvc;
using AbstractConfigurationManager;
using Entities;
using FluentAssertions;
using ItsaWeb.Controllers;
using ItsaWeb.Filters;
using ItsaWeb.Infrastructure;
using ItsaWeb.Models;
using ItsaWebTests.Helpers;
using Logging;
using Moq;
using NUnit.Framework;
using ServiceInterfaces;

namespace ItsaWebTests.Filters
{
    [TestFixture]
    class GetCookieUserFilterAttributeTests
    {
        Mock<HttpContextBase> _httpContext = new Mock<HttpContextBase>();
        Mock<ILogger> _logger = new Mock<ILogger>();
        Mock<IUserService> _service = new Mock<IUserService>();
        Mock<IConfigurationManager> _configurationManager = new Mock<IConfigurationManager>();

        [SetUp]
        public void Setup()
        {
            _httpContext.SetupProperty(h => h.User);
        }

        [Test]
        public void GivenAnInvalidController_WhenAuthorized_ThenThereIsNoUserSet()
        {
            var attribute = new GetCookieUserFilterAttribute();
            var context = new AuthorizationContext();
            context.HttpContext = _httpContext.Object;
            attribute.OnAuthorization(context);
            context.HttpContext.User.Should().BeNull();
        }


        [Test]
        public void GivenNoCookie_WhenAuthorized_ThenThereIsAnEmptyUserSet()
        {
            var attribute = new GetCookieUserFilterAttribute();
            var context = new AuthorizationContext();
            _httpContext.Setup(h => h.Request).Returns(new FakeRequest());
            context.Controller = new BaseController(_logger.Object);
            context.HttpContext = _httpContext.Object;
            attribute.OnAuthorization(context);
            context.HttpContext.User.Should().BeOfType<GenericPrincipal>();
            context.HttpContext.User.As<GenericPrincipal>().Identity.Name.Should().BeBlank();
        }

        [Test]
        public void GivenAValidCookie_WhenAuthorized_ThenThereIsAnEmptyUserSet()
        {
            var attribute = new GetCookieUserFilterAttribute();
            var context = new AuthorizationContext();
            var request = new FakeRequest();
            _service.Setup(s => s.GetRegisteredUser()).Returns(new User{Salt = "saltsalt", Name = "name", Password = "password"});
            _configurationManager.Setup(c => c.AppSetting("keyphrase")).Returns("keyphrase");
            string value =  ToBase64("name".Encrypt("saltsalt", "keyphrase"));
            request.Cookies.Add(new HttpCookie("USER", value));
            _httpContext.Setup(h => h.Request).Returns(request);
            context.Controller = new BaseController(_logger.Object);
            context.HttpContext = _httpContext.Object;
            attribute.UserService = _service.Object;
            attribute.ConfigurationManager = _configurationManager.Object;
            attribute.OnAuthorization(context);
            context.HttpContext.User.Should().BeOfType<GenericPrincipal>();
            context.HttpContext.User.As<GenericPrincipal>().Identity.Name.Should().Be("name");
        }

        private string ToBase64(byte[] bytes)
        {
            return Convert.ToBase64String(bytes);
        }
 
    }
}
