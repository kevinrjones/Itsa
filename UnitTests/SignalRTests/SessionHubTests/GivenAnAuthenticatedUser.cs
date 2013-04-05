using FluentAssertions;
using NUnit.Framework;

namespace SignalRTests.SessionHubTests
{
    [TestFixture]
    public class GivenAnAuthenticatedUser
    {
        [Test]
        public void WhenICheckIfTheUserIsAuthenticated_ThenTrueIsReturned()
        {
            TestableSessionHub hub = new TestableSessionHub("kevin");
            hub.IsAuthenticatedUser().Should().BeTrue();
        }
    }
}
