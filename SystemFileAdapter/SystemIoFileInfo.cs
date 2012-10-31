using System;
using System.IO;
using FileSystemInterfaces;

namespace SystemFileAdapter
{
    public class SystemIoFileInfo : IFileInfo
    {
        public Stream Create(string fileName)
        {
            var file = new FileInfo(fileName);
            return file.Create();
        }

        public void Delete(string fileName)
        {
            var file = new FileInfo(fileName);
            file.Delete();
        }

        public Stream Open(FileMode fileMode, string fileName)
        {
            var file = new FileInfo(fileName);
            return file.Open(fileMode);
        }
    }
}
