using System;

namespace Entities
{
    public class BlogEntry
    {
        public DateTime EntryAddedDate { get; set; }
        public DateTime EntryUpdateDate { get; set; }
        public string Title { get; set; }
        public string Post { get; set; }
        public string[] Tags { get; set; }
    }
}