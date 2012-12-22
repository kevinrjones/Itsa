using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using FluentAssertions;
using ItsaWeb.Filters;
using ItsaWebTests.Helpers;
using Moq;
using NUnit.Framework;

namespace ItsaWebTests.Filters
{
    [TestFixture]
    class HttpBasicUnauthorizedResultTests
    {
        [Test]
        public void WhenTheContextIsNull_ThenAnExceptionIsThrown()
        {
            var result = new HttpBasicUnauthorizedResult();
            Action act = () => result.ExecuteResult(null);
            act.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void WhenTheResultIsExecuted_ThenTheAuthenticateHeaderIsAddedToTheCollection()
        {
            var httpContext = new Mock<HttpContextBase>();
            httpContext.Setup(h => h.Response).Returns(new FakeResponse());
            var result = new HttpBasicUnauthorizedResult();
            var context = new ControllerContext();
            context.HttpContext = httpContext.Object;
            result.ExecuteResult(context);
            context.HttpContext.Response.Headers.Should().Contain("WWW-Authenticate");
        }
    }
}
