using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using ItsaWeb.Models;
using ItsaWeb.Models.Users;
using Moq;
using NUnit.Framework;

namespace ItsaWebTests.Models
{
    [TestFixture]
    class UserViewModelTests
    {
        [Test]
        public void GivenAUserViewModel_ItIsNotInANyRole()
        {
            var model = new UserViewModel();
            model.IsInRole(It.IsAny<string>()).Should().BeFalse();
        }
    }
}
