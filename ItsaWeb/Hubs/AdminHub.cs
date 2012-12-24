using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Entities;
using ItsaWeb.Infrastructure;
using ItsaWeb.Models;
using ServiceInterfaces;

namespace ItsaWeb.Hubs
{
    public class UserHub : AuthenticatingHub
    {
        private readonly IUserService _userService;

        public UserHub(IUserService userService)
        {
            _userService = userService;
        }

        public Task<UserViewModel> GetUser()
        {
            return Task.Factory.StartNew(() =>
                        {
                            var user = _userService.GetRegisteredUser();
                            var model = new UserViewModel { Name = user.Name, IsAuthenticated = true };
                            return model;
                        });
        }
    }

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
                EntryAddedDate = model.EntryAddedDate,
                EntryUpdateDate = model.EntryUpdateDate,
                Body = model.Post,
                Title = model.Title
            };

            _adminService.AddBlogEntry(entry);
        }
    }
}