using FileSystemInterfaces;

namespace SystemFileAdapter
{
    public interface IFileInfoFactory
    {
        IFileInfo CreateFileInfo(string filename);
    }
}