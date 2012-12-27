using System;
using Entities;

namespace ItsaWeb.Models.Posts
{
    public class NewPostViewModel
    {
        public NewPostViewModel()
        {
            
        }
        public NewPostViewModel(Post post)       
        {
            DatePosted = post.EntryAddedDate;
            Title = post.Title;
            Post = post.Body;
            Id = post.Id;
        }

        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Post { get; set; }
        public DateTime DatePosted { get; set; }
    }
}