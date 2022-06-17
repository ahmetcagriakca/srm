using Fix.Environment.FileSystem;
using Fix.Environment.Shell;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Fix.Environment.FeatureManagement
{
    public interface IFeatureContextBuilder
    {
        FeatureContext Build();
        FeatureContext Build(IEnumerable<Feature> features);
    }

    public class FeatureContextBuilder : IFeatureContextBuilder
    {
        private readonly IAssemblyLoader assemblyLoader;
        private readonly IDepedencyContextBuilder depedencyContextBuilder;
        private readonly IFeatureProvider featureFinder;

        public FeatureContextBuilder(
            IAssemblyLoader assemblyLoader,
            IDepedencyContextBuilder depedencyContextBuilder,
            IFeatureProvider featureFinder)
        {
            this.assemblyLoader = assemblyLoader ?? throw new ArgumentNullException(nameof(assemblyLoader));
            this.depedencyContextBuilder = depedencyContextBuilder ?? throw new ArgumentNullException(nameof(depedencyContextBuilder));
            this.featureFinder = featureFinder ?? throw new ArgumentNullException(nameof(featureFinder));
        }

        public FeatureContext Build()
        {
            var features = featureFinder.Get().ToList();
            return Build(features);
        }

        public FeatureContext Build(IEnumerable<Feature> features)
        {
            var items = CreateContextItems(features);
            return new FeatureContext
            {
                Items = items
            };
        }

        private IEnumerable<FeatureContextItem> CreateContextItems(IEnumerable<Feature> features)
        {
            foreach (var feature in features)
            {
                FeatureContextItem item;
                try
                {
                    item = CreateContextItem(feature);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.Write(ex);
                    continue;
                }
                yield return item;
            }
        }

        private FeatureContextItem CreateContextItem(Feature feature)
        {
            var item = new FeatureContextItem(feature);
            item.Items = CreateAssemblyItem(feature.Files);
            return item;
        }

        private IEnumerable<AssemblyItem> CreateAssemblyItem(IEnumerable<FileInfo> files)
        {
            foreach (var file in files)
            {
                var item = CreateAssemblyItem(file);
                yield return item;
            }
        }

        private AssemblyItem CreateAssemblyItem(FileInfo fileInfo)
        {
            var assembly = assemblyLoader.LoadIfNot(fileInfo);
            return new AssemblyItem
            {
                Assembly = assembly,
                DependencyContext = depedencyContextBuilder.Build(assembly)
            };
        }
    }
}
