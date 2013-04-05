using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using FluentAssertions;
using ItsaWeb.Controllers;
using NUnit.Framework;

namespace ItsaWebTests.Controllers
{
    [TestFixture]
    public class GivenAHomeController : ControllerTestsBase
    {
        [Test]
        public void WhenIndexIsCalled_ThenTheIndexViewIsReturned()
        {
            HomeController controller = new HomeController();
            ViewResult result = (ViewResult) controller.Index();
            result.ViewName.Should().Be("");
        }
    }
}
