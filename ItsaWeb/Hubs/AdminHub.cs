using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entities;
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

        public UserViewModel GetUserName()
        {
            try
            {
                var model = new UserViewModel { UserName = UserName, AllowComments = true };
                return model;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void AddEntry(BlogEntryViewModel model)
        {
            var entry = new BlogEntry { EntryAddedDate = model.EntryAddedDate, 
                EntryUpdateDate = model.EntryUpdateDate, 
                Post = model.Post, 
                Title = model.Title };

            _adminService.AddBlogEntry(entry);
        }
    }
}