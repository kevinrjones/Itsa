using System;
using System.IO;

namespace FileSystemInterfaces
{
    public interface IFileInfo
    {
        Stream Create(string fileName);
        void Delete(string fileName);
        Stream Open(FileMode fileMode, string fileName);
    }
}