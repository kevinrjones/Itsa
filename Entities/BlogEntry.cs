using System;

namespace Entities
{
    public class BlogEntry
    {
        public BlogEntry()
        {
            Id = new Guid();
        }
        public Guid Id { get; set; }
        public DateTime EntryAddedDate { get; set; }
        public DateTime EntryUpdateDate { get; set; }
        public string Title { get; set; }
        public string Post { get; set; }
        public string[] Tags { get; set; }
    }
}