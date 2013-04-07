using System;

namespace Entities
{
    public class Post
    {
        public Post()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public DateTime EntryAddedDate { get; set; }
        public DateTime EntryUpdateDate { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string[] Tags { get; set; }
        public bool CommentsEnabled { get; set; }
        public string TitleLink
        {
            get { return Title == null ? "" : Title.Replace(' ', '-').Replace('/', '-').ToLower(); }
        }
        public bool Draft { get; set; }
    }
}