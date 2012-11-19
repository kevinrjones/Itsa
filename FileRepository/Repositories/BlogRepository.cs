using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SystemFileAdapter;
using Entities;
using Exceptions;
using FileSystemInterfaces;
using ItsaRepository.Interfaces;
using Repository;
using Serialization;

namespace FileRepository.Repositories
{
    public class BlogRepository : BaseFileRepository<BlogEntry>, IBlogRepository
    {

        public BlogRepository(string path, IFileInfoFactory fileInfo, IDirectoryInfo directoryInfo) : base(path, fileInfo, directoryInfo)
        {
        }
        protected override string GenerateFileName(BlogEntry entity)
        {
            return string.Format("{0}/{1}.json", Path, entity.Id);
        }
    }
}
