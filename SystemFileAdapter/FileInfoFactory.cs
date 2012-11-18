using FileSystemInterfaces;

namespace SystemFileAdapter
{
    public abstract class FileInfoFactory
    {
        public abstract IFileInfo CreateFileInfo(string filename);
    }
}