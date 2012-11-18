using FileSystemInterfaces;

namespace SystemFileAdapter
{
    public class SystemFileInfoFactory : FileInfoFactory
    {
        public override IFileInfo CreateFileInfo(string filename)
        {
            return new SystemIoFileInfo(filename);
        }
    }
}