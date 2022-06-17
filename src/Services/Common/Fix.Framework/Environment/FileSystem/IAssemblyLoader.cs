using System.IO;
using System.Reflection;

namespace Fix.Environment.FileSystem
{
    public interface IAssemblyLoader
    {
        Assembly LoadIfNot(FileInfo fileInfo);
        //IEnumerable<Assembly> LoadAssemblyFiles(IEnumerable<FileInfo> files);

        //IEnumerable<Assembly> GetAssembliesMatchesFiles(IEnumerable<FileInfo> files);
    }
}
