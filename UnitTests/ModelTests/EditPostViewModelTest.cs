using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using ItsaWeb.Models.Posts;
using NUnit.Framework;

namespace ModelTests
{
    [TestFixture]
    class EditPostViewModelTest
    {
        [Test]
        public void GivenAnEditPostViewModel_WhenAPostIsWithAnUnclosedHTMLElement_ThenTheElementIsCorrectlyClosed()
        {
            var model = new EditPostViewModel();
            model.Post = "<h2>Post";

            model.Post.Should().EndWith("</h2>");
        }

        [Test]
        public void GivenAnEditPostViewModel_WhenAPostIsWithANestedUnclosedHTMLElement_ThenTheElementIsCorrectlyClosed()
        {
            var model = new EditPostViewModel();
            model.Post = "<h2><span>Post</h2>";

            model.Post.Should().EndWith("</span></h2>");
        }
    }
}
