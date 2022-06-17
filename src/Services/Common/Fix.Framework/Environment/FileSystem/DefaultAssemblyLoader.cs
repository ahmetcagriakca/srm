using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Fix.Environment.FileSystem
{
    public class DefaultAssemblyLoader : IAssemblyLoader
    {
        public IEnumerable<Assembly> GetAssembliesMatchesFiles(IEnumerable<FileInfo> files)
        {
            var friendlyName = files.Select(x => Path.GetFileNameWithoutExtension(x.FullName));

            return AppDomain.CurrentDomain.GetAssemblies()
                .ToList()
                .Where(a => friendlyName.Any(f => f == a.GetName().Name));
        }

        private bool IsLoaded(FileInfo fileInfo)
        {
            var name = Path.GetFileNameWithoutExtension(fileInfo.FullName);
            return AppDomain.CurrentDomain.GetAssemblies()
                .ToList()
                .Select(x => x.GetName().Name)
                .Contains(name);
        }

        private bool TryGetAssembly(FileInfo fileInfo, out Assembly assembly)
        {
            var name = Path.GetFileNameWithoutExtension(fileInfo.FullName);
            assembly = AppDomain.CurrentDomain.GetAssemblies().ToList()
                .FirstOrDefault(x => x.GetName().Name == name);
            return !(assembly == null);
        }

        private bool IsNameMatching(Assembly assembly, IEnumerable<string> fileNames)
        {
            return fileNames.Any(x => assembly.GetName().Name == x);
        }

        public Assembly LoadIfNot(FileInfo file)
        {
            try
            {
                if (!TryGetAssembly(file, out Assembly assembly))
                {
                    assembly = Assembly.LoadFrom(file.FullName);
                }
                return assembly;
            }
            catch (Exception ex)
            {
                Debug.Write(ex);
            }
            return null;
        }
    }
}
