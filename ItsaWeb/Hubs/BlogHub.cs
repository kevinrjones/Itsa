using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ItsaWeb.Models;
using ItsaWeb.Models.Posts;
using ServiceInterfaces;
using SignalR.Hubs;

namespace ItsaWeb.Hubs
{
    public class BlogHub : Hub
    {
        private readonly IPostService _blogService;

        public BlogHub(IPostService blogService)
        {
            _blogService = blogService;
        }

        public List<BlogPostViewModel> GetBlogEntries()
        {
            var posts = _blogService.GetPosts();
            return posts.Select(post => new BlogPostViewModel {Title = post.Title, Post = post.Body, Id = post.Id, EntryAddedDate = post.EntryAddedDate, EntryUpdateDate = post.EntryUpdateDate})
                .OrderByDescending(p => p.EntryAddedDate)
                .ToList();
        }
    }
}