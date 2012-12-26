using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entities;
using ItsaWeb.Infrastructure;
using ItsaWeb.Models;
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

        public void AddEntry(BlogEntryViewModel model)
        {
            var user = UserName;

            var entry = new Post
            {
                EntryAddedDate = DateTime.Now,
                EntryUpdateDate = DateTime.Now,
                Body = model.Post,
                Title = model.Title
            };

            _adminService.AddBlogEntry(entry);
        }
    }
}