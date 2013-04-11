using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SystemFileAdapter;
using Exceptions;
using FileSystemInterfaces;
using Repository;
using Serialization;

namespace FileRepository.Repositories
{
    public abstract class BaseFileRepository<T> : IRepository<T> where T : new()
    {
        protected readonly string Path;
        protected readonly IFileInfoFactory FileInfoFactory;
        protected readonly IDirectoryInfo DirectoryInfo;

        protected BaseFileRepository(string path, IFileInfoFactory fileInfo, IDirectoryInfo directoryInfo)
        {
            Path = path;
            FileInfoFactory = fileInfo;
            DirectoryInfo = directoryInfo;
        }

        public void Dispose(){}

        public IQueryable<T> Entities
        {
            get
            {
                var entries = new List<T>();
                // ReSharper disable LoopCanBeConvertedToQuery
                foreach (var fileInfo in DirectoryInfo.EnumerateFiles(Path, "*.json"))
                // ReSharper restore LoopCanBeConvertedToQuery
                {
                    T entry;
                    using (var stream = new StreamReader(fileInfo.Open(FileMode.Open)))
                    {
                        var json = stream.ReadToEnd();
                        entry = JsonSerializer.Deserialize<T>(json);
                    }
                    entries.Add(entry);
                }
                return entries.AsQueryable();
            }
        }

        public T New()
        {
            return new T();
        }

        public void Update(T entity)
        {
            string fileName = GenerateFileName(entity);
            try
            {
                IFileInfo fileInfo = FileInfoFactory.CreateFileInfo(fileName);
                using (var stream = fileInfo.Open(FileMode.Truncate))
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

        public virtual T Create(T entity)
        {
            var fileName = GenerateFileName(entity);
            try
            {
                IFileInfo fileInfo = FileInfoFactory.CreateFileInfo(fileName);

                using (var stream = fileInfo.Create())
                {
                    var json = entity.SerializeToString();
                    var bytes = Encoding.UTF8.GetBytes(json);
                    stream.Write(bytes, 0, bytes.Length);
                }
                return entity;
            }
            catch (IOException)
            {
                throw new RepositoryException(string.Format("Unable to create entry with filename: {0}", fileName));
            }
            catch (Exception e)
            {
                throw new ItsaException("Unable to create entity", e);
            }
        }

        public void Delete(T entity)
        {
            var fileName = GenerateFileName(entity);
            try
            {
                IFileInfo fileInfo = FileInfoFactory.CreateFileInfo(fileName);

                fileInfo.Delete();
            }
            catch (IOException)
            {
                throw new RepositoryException(string.Format("Unable to delete entry with filename: {0}", fileName));
            }
            catch (Exception)
            {
                throw new ItsaException();
            }

        }

        protected abstract string GenerateFileName(T entity);

    }
}
