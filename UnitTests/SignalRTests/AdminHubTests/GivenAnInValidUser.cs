using System;
using FluentAssertions;
using ItsaWeb.Infrastructure;
using ItsaWeb.Models.Posts;
using Moq;
using NUnit.Framework;

namespace SignalRTests
{
    namespace AdminHubTests
    {
        [TestFixture]
        internal class GivenAnInValidUser
        {
            [Test]
            public void WhenAPostIsSentToTheHub_ThenAnExceptionIsThrown()
            {
                var hub = new TestableAdminHub(null, null);
                Action act = () => hub.AddBlogPost(new NewPostViewModel());
                act.ShouldThrow<NotLoggedInException>();
            }

            [Test]
            public void WhenAPostIsDeleted_ThenAnExceptionIsThrown()
            {
                var hub = new TestableAdminHub(null, null);
                Action act = () => hub.DeleteBlogPost(It.IsAny<Guid>());

                act.ShouldThrow<NotLoggedInException>();
            }
        }
    }
}