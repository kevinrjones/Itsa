using System.Collections.Generic;

namespace FileSystemInterfaces
{
    public interface IDirectoryInfo
    {
        IEnumerable<IFileInfo> EnumerateFiles(string path, string filter);
    }
}