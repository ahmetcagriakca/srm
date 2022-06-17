using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Fix.Environment.FeatureManagement
{
    public interface IFeatureProvider
    {
        IEnumerable<Feature> Get();
    }
    public class FeatureProvider : IFeatureProvider
    {
        private readonly IFeatureDirectoryService featureDirectoryService;

        public FeatureProvider(IFeatureDirectoryService featureDirectoryService)
        {
            this.featureDirectoryService = featureDirectoryService ?? throw new ArgumentNullException(nameof(featureDirectoryService));
        }

        public IEnumerable<Feature> Get()
        {
            //var location = new Uri(Assembly.GetCallingAssembly().GetName().CodeBase);
            //var dir = new FileInfo(location.AbsolutePath).Directory;
            //yield return CreateFeature(dir);
            //aca editing
            yield return CreateAppFeature();
            //foreach (var directory in featureDirectoryService.GetDirectories())
            //{
            //    yield return CreateFeature(directory);
            //}
        }

        private Feature CreateFeature(DirectoryInfo directoryInfo)
        {
            var directoryFiles = directoryInfo.GetFiles("*.dll", SearchOption.AllDirectories).ToList();
            var files = directoryFiles.Where(en => en.Name.StartsWith("Fix") || en.Name.StartsWith("SRM.") || en.Name.StartsWith("IdentityServer")).ToList();
            //var paths = files.Select(en => en.FullName);
            //foreach (var item in paths)
            //{
            //   StaticLogger.WriteToFile(item.ToString());
            //}
            return new Feature
            {
                FriendlyName = directoryInfo.Name,
                Files = files
            };
        }

        private Feature CreateAppFeature()
        {

            var location = new Uri(Assembly.GetCallingAssembly().GetName().CodeBase);
            var dir = new FileInfo(location.AbsolutePath).Directory;
            return CreateFeature(dir);
            //var files = new List<FileInfo>
            //{
            //   GetExecutingAppFileInfo(),
            //   GetExecutingFixFileInfo()
            //};
            //return new Feature
            //{
            //    FriendlyName = "AppStartUp",
            //    Files = files
            //};
        }


        public static DirectoryInfo GetExecutingDirectory()
        {
            var location = new Uri(Assembly.GetEntryAssembly().GetName().CodeBase);
            return new FileInfo(location.AbsolutePath).Directory;
        }
        public static FileInfo GetExecutingAppFileInfo()
        {
            var location = new Uri(Assembly.GetEntryAssembly().GetName().CodeBase);
            return new FileInfo(location.AbsolutePath);
        }

        public static FileInfo GetExecutingFixFileInfo()
        {
            var location = new Uri(Assembly.GetCallingAssembly().GetName().CodeBase);
            return new FileInfo(location.AbsolutePath);
        }

    }
}
