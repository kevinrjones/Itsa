using FileSystemInterfaces;

namespace SystemFileAdapter
{
    public class SystemFileInfoFactory : IFileInfoFactory
    {
        public IFileInfo CreateFileInfo(string filename)
        {
            return new SystemIoFileInfo(filename);
        }
    }
}