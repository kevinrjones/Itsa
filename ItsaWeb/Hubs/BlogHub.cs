using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ItsaWeb.Models;
using ItsaWeb.Models.Posts;
using SignalR.Hubs;

namespace ItsaWeb.Hubs
{
    public class BlogHub : Hub
    {
        public List<BlogPostViewModel> GetBlogEntries()
        {
            return new List<BlogPostViewModel>();
        }
    }
}