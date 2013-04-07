using System;
using FluentAssertions;
using ItsaWeb.Infrastructure;
using NUnit.Framework;

namespace SignalRTests.AuthenticatingHubTests
{
    [TestFixture]
    public class AuthenticatingHubTests
    {
        private TestableAuthenticatingHub _hub;

        [SetUp]
        public void SetUp()
        {
        }

        [Test]
        public void GivanAValidUser_ThenTheHubReturnsTheCorrectIdentity()
        {
            _hub = new TestableAuthenticatingHub("Kevin");
            _hub.Identity.Should().Be("Kevin");
        }

        [Test]
        public void GivanNoUser_ThenTheHubThrowsaNotLoggedInException()
        {
            _hub = new TestableAuthenticatingHub(null);
            try
            {
                var foo = _hub.Identity;
            }
            catch (Exception e)
            {
                e.Should().BeOfType<NotLoggedInException>();
                return;
            }
            Assert.Fail("Exception should be thrown");
        }
    }
}