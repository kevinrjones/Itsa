using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace ServiceInterfaces
{
    public interface IMediaService
    {
        Media GetMedia(int year, int month, int day, string linkKey);
        Media GetMedia(Guid mediaId);
        IEnumerable<Media> GetMedia(int pageNumber, int pageItems);
        Media CreateMedia(string fileName, string contentType, Stream inputStream, int contentLength);

        void UpdateMedia(string fileName, string title, string caption, string description, string alternate,
                        string contentType, int alignment, int size, Stream inputStream, int contentLength);

        Media UpdateMediaDetails(Guid id, string fileName, string caption, string description, string alternate);

        void DeleteMedia(Guid mediaId);
    }
}
