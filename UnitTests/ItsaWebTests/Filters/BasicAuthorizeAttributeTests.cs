using System;
using System.Security.Principal;
using System.Text;
using System.Web;
using System.Web.Mvc;
using AbstractConfigurationManager;
using Entities;
using FluentAssertions;
using ItsaWeb.Filters;
using ItsaWebTests.Helpers;
using Moq;
using NUnit.Framework;
using ServiceInterfaces;

namespace ItsaWebTests.Filters
{
    [TestFixture]
    class BasicAuthorizeAttributeTests
    {
        Mock<HttpContextBase> httpContext = new Mock<HttpContextBase>();
        Mock<IUserService> userService = new Mock<IUserService>();
        Mock<IConfigurationManager> configurationManager = new Mock<IConfigurationManager>();

        [Test]
        public void GivenANullFilterContext_ThenAnExceptionIsThrown()
        {
            var attr = new BasicAuthorizeAttribute();
            Action act = () => attr.OnAuthorization(null);
            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void WhenThereIsNoSSL_AndThereIsNoSecureConnection_AndTheRequestIsNotLocal_ThenTheResultIsHttpBasicUnauthorizedResult()
        {
            var filterContext = new AuthorizationContext();
            var attr = new BasicAuthorizeAttribute();
            httpContext.Setup(h => h.Request).Returns(new FakeRequest { SecureConnection = false, Local = false });
            attr.RequireSsl = true;
            filterContext.HttpContext = httpContext.Object;
            attr.OnAuthorization(filterContext);
            filterContext.Result.Should().BeOfType<HttpBasicUnauthorizedResult>();
        }

        [Test]
        public void WhenThereIsNoAuthorizationHeader_ThenTheResultIsHttpBasicUnauthorizedResult()
        {
            var filterContext = new AuthorizationContext();
            var attr = new BasicAuthorizeAttribute();
            attr.RequireSsl = false;
            var request = new FakeRequest { SecureConnection = false, Local = false };
            httpContext.Setup(h => h.Request).Returns(request);

            filterContext.HttpContext = httpContext.Object;
            attr.OnAuthorization(filterContext);
            filterContext.Result.Should().BeOfType<HttpBasicUnauthorizedResult>();
        }

        [Test]
        public void WhenTheBasicCredentialsAreInvalid_ThenTheResultIsHttpBasicUnauthorizedResult()
        {
            userService.Setup(u => u.GetRegisteredUser()).Returns(new User { Salt = "saltsalt", Name = "name", Password = "password" });
            configurationManager.Setup(c => c.AppSetting("keyphrase")).Returns("key");
            var filterContext = new AuthorizationContext();
            var attr = new BasicAuthorizeAttribute();
            attr.UserService = userService.Object;
            attr.ConfigurationManager = configurationManager.Object;
            attr.RequireSsl = false;
            var request = new FakeRequest { SecureConnection = false, Local = false };
            httpContext.Setup(h => h.Request).Returns(request);
            httpContext.Setup(h => h.Response).Returns(new FakeResponse());
            httpContext.SetupProperty(h => h.User);

            request.Values["Authorization"] = "Basi: ";
            filterContext.HttpContext = httpContext.Object;
            attr.OnAuthorization(filterContext);
            filterContext.Result.Should().BeOfType<HttpBasicUnauthorizedResult>();
        }

        [Test]
        public void WhenTheBasicCredentialsHaveInvalidValues_ThenTheResultIsHttpBasicUnauthorizedResult()
        {
            userService.Setup(u => u.GetRegisteredUser()).Returns(new User { Salt = "saltsalt", Name = "name", Password = "password" });
            configurationManager.Setup(c => c.AppSetting("keyphrase")).Returns("key");
            var filterContext = new AuthorizationContext();
            var attr = new BasicAuthorizeAttribute();
            attr.UserService = userService.Object;
            attr.ConfigurationManager = configurationManager.Object;
            attr.RequireSsl = false;
            var request = new FakeRequest { SecureConnection = false, Local = false };
            httpContext.Setup(h => h.Request).Returns(request);
            httpContext.Setup(h => h.Response).Returns(new FakeResponse());
            httpContext.SetupProperty(h => h.User);

            request.Values["Authorization"] = "Basic: ";
            filterContext.HttpContext = httpContext.Object;
            attr.OnAuthorization(filterContext);
            filterContext.Result.Should().BeOfType<HttpBasicUnauthorizedResult>();
        }

        [Test]
        public void WhenThereIsNoPrinciple_ThenTheResultIsHttpBasicUnauthorizedResult()
        {
            userService.Setup(u => u.GetRegisteredUser()).Returns(new User { Salt = "saltsalt", Name = "name", Password = "password" });
            configurationManager.Setup(c => c.AppSetting("keyphrase")).Returns("key");
            var filterContext = new AuthorizationContext();
            var attr = new BasicAuthorizeAttribute();
            attr.UserService = userService.Object;
            attr.ConfigurationManager = configurationManager.Object;
            attr.RequireSsl = false;
            var request = new FakeRequest { SecureConnection = false, Local = false };
            httpContext.Setup(h => h.Request).Returns(request);
            httpContext.Setup(h => h.Response).Returns(new FakeResponse());
            httpContext.SetupProperty(h => h.User);

            request.Values["Authorization"] = "Basic: " + ToBase64("name", "invalid");
            filterContext.HttpContext = httpContext.Object;
            attr.OnAuthorization(filterContext);
            filterContext.Result.Should().BeOfType<HttpBasicUnauthorizedResult>();
        }

        [Test]
        public void WhenThereIsAPrinciple_ThenTheUserIsStoredInTheContext()
        {
            userService.Setup(u => u.GetRegisteredUser()).Returns(new User { Salt = "saltsalt", Name = "name", Password = "password" });
            configurationManager.Setup(c => c.AppSetting("keyphrase")).Returns("key");
            var filterContext = new AuthorizationContext();
            var attr = new BasicAuthorizeAttribute();
            attr.UserService = userService.Object;
            attr.ConfigurationManager = configurationManager.Object;
            attr.RequireSsl = false;
            var request = new FakeRequest { SecureConnection = false, Local = false };
            httpContext.Setup(h => h.Request).Returns(request);
            httpContext.Setup(h => h.Response).Returns(new FakeResponse());
            httpContext.SetupProperty(h => h.User);

            request.Values["Authorization"] = "Basic: " + ToBase64("name", "password");
            filterContext.HttpContext = httpContext.Object;
            attr.OnAuthorization(filterContext);
            filterContext.HttpContext.User.Should().BeOfType<GenericPrincipal>();
        }

        [Test]
        public void WhenThereIsAPrinciple_AndTheyAreNotAuthorized_ThenTheResultIsHttpBasicUnauthorizedResult()
        {
            userService.Setup(u => u.GetRegisteredUser()).Returns(new User { Salt = "saltsalt", Name = "name", Password = "password" });
            configurationManager.Setup(c => c.AppSetting("keyphrase")).Returns("key");
            var filterContext = new AuthorizationContext();
            var attr = new BasicAuthorizeAttribute();
            attr.UserService = userService.Object;
            attr.ConfigurationManager = configurationManager.Object;
            attr.RequireSsl = false;
            var request = new FakeRequest { SecureConnection = false, Local = false };
            httpContext.Setup(h => h.Request).Returns(request);
            httpContext.Setup(h => h.Response).Returns(new FakeResponse());
            httpContext.SetupProperty(h => h.User);

            request.Values["Authorization"] = "Basic: " + ToBase64("name", "password");
            filterContext.HttpContext = httpContext.Object;
            attr.OnAuthorization(filterContext);
            filterContext.HttpContext.User.Should().BeOfType<GenericPrincipal>();
        }

        private string ToBase64(string name, string password)
        {
            return Convert.ToBase64String(Encoding.ASCII.GetBytes(name + ":" + password));
        }
    }

}