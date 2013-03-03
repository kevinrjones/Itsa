using System;
using System.Collections.Generic;
using System.IO;

namespace FileSystemInterfaces
{
    public interface IFileInfo
    {
        Stream Create();
        void Delete();
        Stream Open(FileMode fileMode);
        bool Exists { get; }
    
    }
}