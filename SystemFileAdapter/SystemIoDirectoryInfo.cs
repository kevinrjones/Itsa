using System.Collections.Generic;
using System.IO;
using FileSystemInterfaces;

namespace SystemFileAdapter
{
    public class SystemIoDirectoryInfo : IDirectoryInfo
    {
        public IEnumerable<IFileInfo> EnumerateFiles(string path, string filter)
        {
            DirectoryInfo di = new DirectoryInfo(path);
            foreach (var fileInfo in di.EnumerateFiles(filter))
            {
                yield return new SystemIoFileInfo(fileInfo.Name);
            }            
        }
    }
}