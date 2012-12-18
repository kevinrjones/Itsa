using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using ItsaWeb.Models.Media;
using HtmlAgilityPack;

namespace ItsaWeb.Models.Posts
{
    public class EditPostViewModel 
    {
        private string _post;
        public Guid PostId { get; set; }
        public DateTime Edited { get; set; }
        public DateTime Published { get; set; }

        [Required]
        public string Title { get; set; }


        [Required]
        public string Post
        {
            get { return _post; }
            set
            {
                var doc = new HtmlDocument
                              {
                                  OptionAutoCloseOnEnd = false,
                                  OptionFixNestedTags = true,
                                  OptionWriteEmptyNodes = true
                              };

                doc.LoadHtml(value);
                var writer = new StringWriter();
                doc.Save(writer);
                _post = writer.ToString();
            }
        }

        public bool IsCreate { get; set; }
        public NewMediaViewModel NewMediaViewModel { get; set; }
    }
}