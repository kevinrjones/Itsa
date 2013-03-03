using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using FluentAssertions;
using ItsaWeb.Models.Posts;
using NUnit.Framework;

namespace ModelTests
{
    [TestFixture]
    class NewPostViewModelTests
    {
        [Test]
        public void WhenANewPostVewModelIsCreated_ThenTheFieldsAreSetCorrectly()
        {
            var id = Guid.NewGuid();
            var post = new Post { EntryAddedDate = new DateTime(1977,1,1), Body = "body", Title = "title", Id = id};
            var model = new NewPostViewModel(post);
            model.DatePosted.Should().Be(new DateTime(1977, 1, 1));
            model.Id.Should().Be(id);
            model.Post.Should().Be("body");
            model.Title.Should().Be("title");
        }
    }
}
