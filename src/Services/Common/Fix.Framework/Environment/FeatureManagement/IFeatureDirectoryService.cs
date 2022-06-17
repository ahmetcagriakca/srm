using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Fix.Environment.FeatureManagement
{
    public interface IFeatureDirectoryService
    {
        IEnumerable<DirectoryInfo> GetDirectories();
    }
    public class FeatureDirectoryService : IFeatureDirectoryService
    {
        private readonly IFeaturePathProvider featurePathProvider;

        public FeatureDirectoryService(IFeaturePathProvider featurePathProvider)
        {
            this.featurePathProvider = featurePathProvider ?? throw new ArgumentNullException(nameof(featurePathProvider));
        }
        public IEnumerable<DirectoryInfo> GetDirectories()
        {
            var root = featurePathProvider.GetRoot();


            return root.GetDirectories("*.*", SearchOption.AllDirectories).Where(x => CheckFeatureFolder(x));
        }

        private bool CheckFeatureFolder(DirectoryInfo directoryInfo)
        {
            foreach (var item in GetExceptFoldersPrefix())
            {
                if (directoryInfo.Name.StartsWith(item))
                    return false;
            }
            return true;
        }

        private IEnumerable<string> GetExceptFoldersPrefix()
        {
            yield return "netstandard";
            yield return "netcoreapp";
        }


    }
}
