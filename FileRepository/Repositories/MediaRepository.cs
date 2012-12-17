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
using ItsaRepository.interfaces;
using Repository;
using Serialization;

namespace FileRepository.Repositories
{
    public class MediaRepository : BaseFileRepository<Media>, IMediaRepository
    {

        public MediaRepository(string path, IFileInfoFactory fileInfo, IDirectoryInfo directoryInfo) : base(path + "/media", fileInfo, directoryInfo)
        {
        }

        protected override string GenerateFileName(Media entity)
        {
            return GenerateFileName(entity.Id);
        }

        private string GenerateFileName(Guid id)
        {
            return string.Format("{0}/{1}.json", Path, id);
        }

    }
}
