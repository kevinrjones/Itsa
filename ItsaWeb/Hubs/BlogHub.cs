using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entities;
using Exceptions;
using ItsaWeb.Models;
using ItsaWeb.Models.Posts;
using Microsoft.AspNet.SignalR;
using ServiceInterfaces;

namespace ItsaWeb.Hubs
{
    public class BlogHub : AuthenticatingHub
    {
        private readonly IPostService _blogService;

        public BlogHub(IPostService blogService)
        {
            _blogService = blogService;
        }

        public List<BlogPostViewModel> List()
        {
            var posts = _blogService.GetPosts();
            return posts.Select(post => new BlogPostViewModel { Title = post.Title, Body = post.Body, Id = post.Id, EntryAddedDate = post.EntryAddedDate, EntryUpdateDate = post.EntryUpdateDate })
                .OrderByDescending(p => p.EntryAddedDate)
                .ToList();
        }

        public BlogPostViewModel Get(Guid id)
        {
            var post = _blogService.GetPost(id);
            return  new BlogPostViewModel { Title = post.Title, Body = post.Body, Id = post.Id, EntryAddedDate = post.EntryAddedDate, EntryUpdateDate = post.EntryUpdateDate };
        }

        public BlogPostViewModel Create(BlogPostViewModel model)
        {
            IsAuthenticated();
            if (string.IsNullOrEmpty(model.Title))
            {
                throw new ItsaException("All posts must have a title");
            }
            var post = _blogService.CreatePost(new Post{Body = model.Body, Title = model.Title, Tags = model.Tags, EntryAddedDate = DateTime.Now, CommentsEnabled = model.CommentsEnabled, Draft = model.IsDraft});
            model.Id = post.Id;
            return model;
        }

        public BlogPostViewModel Update(BlogPostViewModel model)
        {
            IsAuthenticated();
            _blogService.UpdatePost(new Post{Body = model.Body, Title = model.Title, Tags = model.Tags, CommentsEnabled = model.CommentsEnabled, Draft = model.IsDraft, EntryUpdateDate = DateTime.Now});
            return model;
        }
    }
}