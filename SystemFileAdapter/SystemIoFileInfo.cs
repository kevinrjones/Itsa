using System;
using System.IO;
using FileSystemInterfaces;

namespace SystemFileAdapter
{
    public class SystemIoFileInfo : IFileInfo
    {
        private readonly string _fileName;

        public SystemIoFileInfo(string fileName)
        {
            this._fileName = fileName;
        }

        public Stream Create()
        {
            var file = new FileInfo(_fileName);
            if (file.Exists)
            {
                throw new IOException("File already exists");
            }
            return file.Create();
        }

        public void Delete()
        {
            var file = new FileInfo(_fileName);
            file.Delete();
        }

        public Stream Open(FileMode fileMode)
        {
            var file = new FileInfo(_fileName);
            return file.Open(fileMode);
        }
    }
}
