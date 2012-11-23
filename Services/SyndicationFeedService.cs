using System;
using System.Collections.Generic;
using System.ServiceModel.Syndication;
using Entities;
using ItsaRepository.Interfaces;
using ServiceInterfaces;

namespace MBlogService
{
    public class SyndicationFeedService : ISyndicationFeedService
    {
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;

        public SyndicationFeedService(IPostRepository postRepository, IUserRepository userRepository)
        {
            _postRepository = postRepository;
            _userRepository = userRepository;
        }

        #region ISyndicationFeedService Members

        public SyndicationFeed CreateSyndicationFeed(string feedType, string scheme, string host)
        {
            IList<Post> posts = _postRepository.GetPosts();
            var user = _userRepository.GetUser();
            
            string url = string.Format("{0}://{1}", scheme, host);
            var feed = new SyndicationFeed(user.BlogTitle, user.BlogDescription, new Uri(url), url, user.LastUpdated);
            feed.Authors.Add(new SyndicationPerson { Name = user.Name });
            
            feed.Links.Add(SyndicationLink.CreateSelfLink(new Uri(url + "/feed/" + feedType)));

            var items = new List<SyndicationItem>();
            foreach (Post post in posts)
            {
                var htmlurl = string.Format("{0}://{1}/{2}/{3}/{4}/{5}", scheme, host, post.EntryAddedDate.Year,
                                    post.EntryAddedDate.Month, post.EntryAddedDate.Day, post.TitleLink);

                var item = new SyndicationItem();
                item.Title = new TextSyndicationContent(post.Title, TextSyndicationContentKind.Html);
                item.Content = new TextSyndicationContent(post.Body, TextSyndicationContentKind.Html);
                item.Links.Add(SyndicationLink.CreateAlternateLink(new Uri(htmlurl), "text/html"));
                
                var editurl = string.Format("{0}://{1}/pub/atom/{2}", scheme, host, post.Id);
                item.Links.Add(SyndicationLink.CreateSelfLink(new Uri(editurl)));
                item.Links.Add(new SyndicationLink { RelationshipType = "edit", Uri = new Uri(editurl), MediaType = "application/atom+xml;type=entry" });

                item.PublishDate = post.EntryUpdateDate;
                item.Id = editurl;
                items.Add(item);                
            }
            feed.Items = items;
            return feed;
        }
        #endregion
    }
}