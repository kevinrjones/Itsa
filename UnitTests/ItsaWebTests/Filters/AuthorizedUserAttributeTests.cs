using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using FluentAssertions;
using ItsaWeb.Filters;
using ItsaWeb.Models;
using Moq;
using NUnit.Framework;

namespace ItsaWebTests.Filters
{
    [TestFixture]
    class AuthorizedUserAttributeTests
    {
        [Test]
        public void WhenTheUserIsNotAuthorized_TheResultShuoldBeARedirectResult()
        {
            var filterContext = new AuthorizationContext();
            var attr = new TestableAuthorizedUserAttribute();
            attr.HandleUnauthorizedRequest(filterContext);
            filterContext.Result.Should().BeOfType<RedirectResult>();
            filterContext.Result.As<RedirectResult>().Url.Should().Be("~/session/new");
        }

        [Test]
        public void WhenTheUserIsNotAGenericPrincipal_ThenFalseIsReturned()
        {
            var attr = new TestableAuthorizedUserAttribute();
            var httpContextBase = new Mock<HttpContextBase>();
            var result = attr.AuthorizeCore(httpContextBase.Object);
            result.Should().BeFalse();
        }

        [Test]
        public void WhenTheUserIsAGenericPrincipal_ThenTrueIsReturned()
        {
            var attr = new TestableAuthorizedUserAttribute();
            var httpContextBase = new Mock<HttpContextBase>();
            httpContextBase.Setup(h => h.User).Returns(new GenericPrincipal(new UserViewModel(), null));
            var result = attr.AuthorizeCore(httpContextBase.Object);
            result.Should().BeTrue();
        }
    }

    internal class TestableAuthorizedUserAttribute : AuthorizedUserAttribute
    {
        public void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            base.HandleUnauthorizedRequest(filterContext);
        }

        public bool AuthorizeCore(HttpContextBase httpContext)
        {
            return base.AuthorizeCore(httpContext);
        }
    }
}
