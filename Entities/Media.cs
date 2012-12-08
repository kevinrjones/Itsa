using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;

namespace Entities
{
    public class Media
    {
        #region ValidAllignments enum

        public enum ValidAllignments
        {
            None,
            Left,
            Right
        };

        #endregion

        #region ValidSizes enum

        public enum ValidSizes
        {
            Thumbnail,
            Medium,
            Large,
            Fullsize
        };

        #endregion

        private int _alignment;
        private int _size;
        private string _title;

        public Media()
        {
            DateTime date = DateTime.Now;
            Year = date.Year;
            Month = date.Month;
            Day = date.Day;
        }

        public Media(string fileName, string title, string caption,
                     string description, string alternate, 
                     string mimeType, int alignment, int size, byte[] imageData)
            : this()
        {
            FileName = fileName;
            if (!string.IsNullOrEmpty(title))
            {
                Title = title;
            }
            else
            {
                Title = FileName.Split('.').First();
            }
            Caption = caption;
            Description = description;
            Alternate = alternate;
            MimeType = mimeType;
            Alignment = alignment;
            Size = size;
            Data = imageData;
        }

        public Media(string fileName, string contentType, Stream inputStream, int contentLength)
            : this(fileName, "", "", "", "", contentType, 0, 0, null)
        {
            Data = new byte[contentLength];
            inputStream.Read(Data, 0, contentLength);
        }

        [Key]
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                LinkKey = _title.Replace(" ", "");
            }
        }

        public string Caption { get; set; }
        public string Description { get; set; }
        public string Alternate { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public string MimeType { get; set; }
        public int Alignment
        {
            get { return _alignment; }
            set
            {
                if (Enum.IsDefined(typeof (ValidAllignments), value))
                {
                    _alignment = value;
                }
                else
                {
                    _alignment = (int) ValidAllignments.None;
                }
            }
        }
        public int Size
        {
            get { return _size; }
            set
            {
                if (Enum.IsDefined(typeof (ValidSizes), value))
                {
                    _size = value;
                }
                else
                {
                    _size = (int) ValidSizes.Fullsize;
                }
            }
        }

        public byte[] Data { get; set; }
        public string LinkKey { get; set; }
        public string Url
        {
            get { return string.Format("{0}/{1}/{2}/{3}", Year, Month, Day, LinkKey); }
        }

        // override object.Equals
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (Media)) return false;
            return Equals((Media) obj);
        }

        public bool Equals(Media other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.Id == Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}