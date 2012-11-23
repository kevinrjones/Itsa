using System;
using Entities;

namespace ItsaWeb.Views.Feed
{
    public class NewPostViewModel
    {
        public NewPostViewModel(Post post)       
        {
            DatePosted = post.EntryAddedDate;
            Title = post.Title;
            Post = post.Body;
        }

        public string Title { get; set; }
        public string Post { get; set; }
        public DateTime DatePosted { get; set; }
        public int Id { get; set; }
    }
}