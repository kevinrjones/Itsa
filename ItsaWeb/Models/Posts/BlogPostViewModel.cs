using System;
using Entities;

namespace ItsaWeb.Models.Posts
{
    public class BlogPostViewModel
    {
        public BlogPostViewModel()
        {
            
        }

        public BlogPostViewModel(Post post)
        {
            Id = post.Id;
            Title = post.Title;
            Post = post.Body;
            EntryAddedDate = post.EntryAddedDate;
            EntryUpdateDate = post.EntryUpdateDate;
        }
        public DateTime EntryAddedDate { get; set; }
        public DateTime EntryUpdateDate { get; set; }
        public string Title { get; set; }
        public string Post { get; set; }
        public string[] Tags { get; set; }
        public Guid Id { get; set; }
        public bool CommentsEnabled { get; set; }
        public bool Draft { get; set; }
    }
}