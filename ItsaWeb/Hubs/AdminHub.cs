using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entities;
using ItsaWeb.Infrastructure;
using ItsaWeb.Models;
using ItsaWeb.Models.Posts;
using ServiceInterfaces;

namespace ItsaWeb.Hubs
{
    public class AdminHub : AuthenticatingHub
    {
        private readonly IAdminService _adminService;

        public AdminHub(IAdminService adminService)
        {
            _adminService = adminService;
        }

        public BlogPostViewModel AddBlogPost(NewPostViewModel model)
        {
            var authenticated = IsAuthenticated();
            {
                var entry = new Post
                                {
                                    EntryAddedDate = DateTime.Now,
                                    EntryUpdateDate = DateTime.Now,
                                    Body = model.Post,
                                    Title = model.Title
                                };

                var added = _adminService.AddBlogPost(entry);
                return new BlogPostViewModel(added);
            }
        }

        public void DeleteBlogPost(Guid id)
        {
            if (IsAuthenticated())
            {
                _adminService.DeleteBlogPost(id);
            }
        }
    }
}