using System;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceModel.Syndication;
using System.Web.Mvc;
using System.Xml.Linq;
using Entities;
using ItsaWeb.ActionResults;
using ItsaWeb.Filters;
using ItsaWeb.Models.Atom;
using ItsaWeb.Models.Posts;
using ItsaWeb.Views.Feed;
using Logging;
using MBlog.ActionResults;
using ServiceInterfaces;

namespace ItsaWeb.Controllers.Publishing
{
    [BasicAuthorize]
    public class AtomController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IPostService _postService;
        private readonly ISyndicationFeedService _syndicationFeedService;

        public AtomController(IUserService userService, IPostService postService, ILogger logger, ISyndicationFeedService syndicationFeedService)
            : base(logger)
        {
            _userService = userService;
            _postService = postService;            
            _syndicationFeedService = syndicationFeedService;
        }

        [HttpGet]
        public virtual ActionResult GetServiceDocument()
        {
            var user = _userService.GetRegisteredUser();
            var viewModel = new AtomViewModel { Title = user.BlogTitle};
            return View(viewModel);
        }

        [HttpGet]
        public virtual ActionResult Index()
        {
            SyndicationFeed feed = _syndicationFeedService.CreateSyndicationFeed("atom",
                                                                     HttpContext.Request.Url.Scheme,
                                                                     HttpContext.Request.Headers["HOST"]);
            return new SyndicationActionResult(feed.GetAtomFeed());

        }

        [HttpGet]
        [AuthorizedUser]
        public virtual ActionResult Get(Guid postId)
        {
            Post post = _postService.Get(postId);
            return
                View(new EditPostViewModel {PostId = postId, Title = post.Title, Post = post.Body, Edited = post.EntryUpdateDate, Published = post.EntryAddedDate});
        }
        
        [HttpPut]
        [AuthorizedUser]
        public virtual ActionResult Update(Guid postId)
        {
            var atomXMl = XDocument.Load(new StreamReader(Request.InputStream));
            XNamespace ns = "http://www.w3.org/2005/Atom";
            var title = (from node in atomXMl.Descendants(ns + "title")
                           select node.Value).FirstOrDefault();
            var content = (from node in atomXMl.Descendants(ns + "content")
                           select node.Value).FirstOrDefault();
            _postService.Update(postId, title, content);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [HttpDelete]
        [AuthorizedUser]
        public virtual ActionResult Delete(Guid postId)
        {
            _postService.Delete(postId);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [HttpPost]
        [AuthorizedUser]
        public virtual ActionResult Create()
        {
            var atomXMl = XDocument.Load(new StreamReader(Request.InputStream));
            XNamespace ns = "http://www.w3.org/2005/Atom";
            var title = (from node in atomXMl.Descendants(ns + "title")
                         select node.Value).FirstOrDefault();
            var content = (from node in atomXMl.Descendants(ns + "content")
                           select node.Value).FirstOrDefault();
            Post post = new Post
            {
                Body = content,
                Title = title,
                EntryUpdateDate = DateTime.UtcNow,
                EntryAddedDate = DateTime.UtcNow,
                CommentsEnabled = true,
            };
            _postService.CreatePost(post);
            return View(new NewPostViewModel(post));
        }
    }
}
