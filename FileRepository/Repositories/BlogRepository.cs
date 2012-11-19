using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SystemFileAdapter;
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
        private readonly string _path;
        private readonly FileInfoFactory _fileInfoFactory;
        private readonly IDirectoryInfo _directoryInfo;

        public BlogRepository(string path, FileInfoFactory fileInfo, IDirectoryInfo directoryInfo)
        {
            _path = path;
            _fileInfoFactory = fileInfo;
            _directoryInfo = directoryInfo;
        }

        public void Dispose()
        {
        }

        public IQueryable<BlogEntry> Entities
        {
            get
            {
                var entries = new List<BlogEntry>();
// ReSharper disable LoopCanBeConvertedToQuery
                foreach (var fileInfo in _directoryInfo.EnumerateFiles(_path, "*.json"))
// ReSharper restore LoopCanBeConvertedToQuery
                {
                    BlogEntry entry;
                    using (var stream = new StreamReader(fileInfo.Open(FileMode.Open)))
                    {
                        var json = stream.ReadToEnd();
                        entry = JsonSerializer.Deserialize<BlogEntry>(json);
                    }
                    entries.Add(entry);                    
                }
                return entries.AsQueryable();
            }
        }
        public BlogEntry New()
        {
            return new BlogEntry();
        }

        public void Update(BlogEntry entity)
        {
            string fileName = GenerateFileName(entity);
            IFileInfo fileInfo = _fileInfoFactory.CreateFileInfo(fileName);
            try
            {
                using (var stream = fileInfo.Open(FileMode.Open))
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
            var fileName = GenerateFileName(entity);
            IFileInfo fileInfo = _fileInfoFactory.CreateFileInfo(fileName);
            
            try
            {
                using (var stream = fileInfo.Create())
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
            var fileName = GenerateFileName(entity);
            IFileInfo fileInfo = _fileInfoFactory.CreateFileInfo(fileName);
            
            try
            {
                fileInfo.Delete();
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

        private string GenerateFileName(BlogEntry entity)
        {
            return string.Format("{0}/{1}.json", _path, entity.Id);
        }
    }
}
