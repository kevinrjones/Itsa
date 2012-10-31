using System;
using System.IO;
using System.Linq;
using System.Text;
using Entities;
using Exceptions;
using FileSystemInterfaces;
using ItsaRepository.interfaces;
using Repository;
using Serialization;

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
            string fileName = string.Format("{0}/{1}-{2}-{3}-{4}-{5}.json", _path, entity.Title, entity.EntryAddedDate.Year, entity.EntryAddedDate.Month, entity.EntryAddedDate.Day, entity.EntryAddedDate.Ticks);

            try
            {
                using (var stream = _fileInfo.Open(FileMode.Open, fileName))
                {
                    var json = entity.SerializeToString();
                    var bytes = Encoding.UTF8.GetBytes(json);
                    stream.Write(bytes, 0, bytes.Length);
                }
            }
            catch (IOException)
            {
                throw new RepositoryException(string.Format("Unable to find entry with filename: {0}", fileName));
            }
            catch (Exception)
            {
                throw new ItsaException();
            }
        }

        public void Create(BlogEntry entity)
        {
            string fileName = string.Format("{0}/{1}-{2}-{3}-{4}-{5}.json", _path, entity.Title, entity.EntryAddedDate.Year, entity.EntryAddedDate.Month, entity.EntryAddedDate.Day, entity.EntryAddedDate.Ticks);
            try
            {
                using (var stream = _fileInfo.Create(fileName))
                {
                    var json = entity.SerializeToString();
                    var bytes = Encoding.UTF8.GetBytes(json);
                    stream.Write(bytes, 0, bytes.Length);
                }
            }
            catch (IOException)
            {
                throw new RepositoryException(string.Format("Unable to create entry with filename: {0}", fileName));
            }
            catch (Exception)
            {
                throw new ItsaException();
            }
        }

        public void Delete(BlogEntry entity)
        {
            string fileName = string.Format("{0}/{1}-{2}-{3}-{4}-{5}.json", _path, entity.Title, entity.EntryAddedDate.Year, entity.EntryAddedDate.Month, entity.EntryAddedDate.Day, entity.EntryAddedDate.Ticks);
            try
            {
                _fileInfo.Delete(fileName);
            }
            catch (IOException)
            {
                throw new RepositoryException(string.Format("Unable to create entry with filename: {0}", fileName));
            }
            catch(Exception)
            {
                throw new ItsaException();
            }
        }
    }
}
