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

        protected bool Equals(Post other)
        {
            return string.Equals(Title, other.Title) && string.Equals(Body, other.Body);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Post) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Title != null ? Title.GetHashCode() : 0)*397) ^ (Body != null ? Body.GetHashCode() : 0);
            }

        }
    }
}