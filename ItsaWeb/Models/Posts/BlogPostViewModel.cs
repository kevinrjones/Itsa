using System;

namespace ItsaWeb.Models.Posts
{
    public class BlogPostViewModel
    {
        public DateTime EntryAddedDate { get; set; }
        public DateTime EntryUpdateDate { get; set; }
        public string Title { get; set; }
        public string Post { get; set; }
        public string[] Tags { get; set; }
    }
}