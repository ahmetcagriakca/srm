using Microsoft.DotNet.PlatformAbstractions;
using System.IO;
using System.Linq;

namespace Fix.Environment.FeatureManagement
{
    public interface IFeaturePathProvider
    {
        DirectoryInfo GetRoot();
    }

    public class FeaturePathProvider : IFeaturePathProvider
    {
        public DirectoryInfo GetRoot()
        {
            var path = ApplicationEnvironment.ApplicationBasePath;
            var directory = new DirectoryInfo(path);

            var module = FindDirectory(directory, "Features");
            if (module == null)
                throw new DirectoryNotFoundException("Features directory not exist.");

            return module;
        }


        private DirectoryInfo FindDirectory(DirectoryInfo directory, string directoryName)
        {
            var target = directory.GetDirectories(directoryName, SearchOption.TopDirectoryOnly).ToList().FirstOrDefault();
            if (target == null && directory.Parent != null)
            {
                target = FindDirectory(directory.Parent, directoryName);
            }
            return target;
        }
    }

}
