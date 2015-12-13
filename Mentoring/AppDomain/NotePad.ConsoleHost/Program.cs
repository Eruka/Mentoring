using Helpers.PluginsDiscover;
using NotePad.Plugins.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NotePad
{
    class Program
    {
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve += new ResolveEventHandler(CurrentDomain_ReflectionOnlyAssemblyResolve);

            var names = PluginDiscover.GetAvailablePlagins(AppDomain.CurrentDomain.BaseDirectory + "\\Plugins", typeof(INotePadPlagin));
            if (names.Any())
            {
                names.ForEach(name => Console.WriteLine(name));
            }
            Console.ReadKey();
        }

        static Assembly CurrentDomain_ReflectionOnlyAssemblyResolve(object sender, ResolveEventArgs args)
        {
            return System.Reflection.Assembly.ReflectionOnlyLoad(args.Name);
        }
    }
}
