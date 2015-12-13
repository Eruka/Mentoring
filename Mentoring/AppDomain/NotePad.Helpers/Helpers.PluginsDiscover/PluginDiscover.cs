using System;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace Helpers.PluginsDiscover
{
    public class PluginDiscover
    {
        public static bool IsPluginImplementInterface(string name, Type type)
        {
            Assembly asm = Assembly.ReflectionOnlyLoad(name);
            var results = asm.GetTypes().Where(t => 
                t.IsPublic && t.IsMarshalByRef && (t.GetInterface(type.Name) != null)
            );
            return results.Any();
        }


        public static List<string> GetAvailablePlagins(string basePath, Type type)
        {
            var fileNames = Directory.EnumerateFiles(basePath, "*.dll")
                .Select(f=>Path.GetFileNameWithoutExtension(f));
            var results = new List<string>();

            if (fileNames.Any())
            {
                foreach (string assemblyFileName in fileNames)
                {
                    if (IsPluginImplementInterface(assemblyFileName, type))
                    {
                        results.Add(assemblyFileName);
                    }
                }
            }
            return results;
        }
    }
}
