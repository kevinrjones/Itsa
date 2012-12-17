using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using FluentAssertions;
using NUnit.Framework;

namespace ModelTests
{
    [TestFixture]
    public class UserTest
    {
        [Test]
        public void GivenAUser_WhenThePasswordIsSet_ThenThePasswordIsHashed()
        {
            var u = new User();
            u.Salt = "wibblefishhatstand";
            u.Password = "password";
            u.HashedPassword.Should().NotBeNullOrEmpty();
        }

        [Test]
        public void GivenAUser_WhenThePasswordIsSet_ThenTheSaltCannotBeNull()
        {
            var u = new User();
            Action act = () => u.Password = "password";
            act.ShouldThrow<ArgumentException>();
        }

        [Test]
        public void GivenAUser_WhenTheHashedPasswordIsSetInTheConstructor_ThenTheSamePasswordIsSetInThePasswordAccessor()
        {
            var u = new User("name", "email", "password");
            var hashedPassword = u.HashedPassword;
            u.Password = "password";

            hashedPassword.Should().Be(u.HashedPassword);
        }

        [Test]
        public void GivenAUser_WhenThePasswordHasBeenSet_AndTheSamePasswordIsCompared_ThenTheComparisonSucceeds()
        {
            var u = new User("name", "email", "password");
            u.MatchPassword("password").Should().BeTrue();
        }

        [Test]
        public void GivenAUser_WhenThePasswordHasBeenSet_AndADifferentPasswordIsCompared_ThenTheComparisonFails()
        {
            var u = new User("name", "email", "password");
            u.MatchPassword("password1").Should().BeFalse();
        }

        [Test]
        public void GivenAUser_WhenTheHashedPasswordIsSetInTheConstructor_AndADifferentPasswordIsSetInThePasswordAccessor_ThenThePasswordsAreDifferent()
        {
            var u = new User("name", "email", "password");
            var hashedPassword = u.HashedPassword;
            u.Password = "password1";

            hashedPassword.Should().NotBe(u.HashedPassword);
        }
    }
}
