using System.IO;
using System.Linq;
using Entities;
using FileSystemInterfaces;
using ItsaRepository.interfaces;

namespace FileRepository.Repositories
{
    public class BlogRepository : IBlogRepository
    {
        private string _path;
        private readonly IFileInfo _fileInfo;

        public BlogRepository(string path, IFileInfo fileInfo)
        {
            _path = path;
            _fileInfo = fileInfo;
        }

        public void Dispose()
        {
        }

        public IQueryable<BlogEntry> Entities { get; private set; }
        public BlogEntry New()
        {
            return new BlogEntry();
        }

        public void Update(BlogEntry entity)
        {
            throw new System.NotImplementedException();
        }

        public void Create(BlogEntry entity)
        {
            string fileName = entity.Title + entity.EntryAddedDate.ToLongDateString();
            File.Create(fileName);
        }

        public void Delete(BlogEntry entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
