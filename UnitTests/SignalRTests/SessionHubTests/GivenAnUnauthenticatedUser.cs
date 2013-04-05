using FluentAssertions;
using NUnit.Framework;

namespace SignalRTests.SessionHubTests
{
    [TestFixture]
    public class GivenAnUnauthenticatedUser
    {
        [Test]
        public void WhenICheckIfTheUserIsAuthenticated_ThenFalseIsReturned()
        {
            TestableSessionHub hub = new TestableSessionHub(null);
            hub.IsAuthenticatedUser().Should().BeFalse();
        }
    }
}