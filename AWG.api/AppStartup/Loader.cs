using System.Collections.Generic;
using System.Composition;
using System.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using AWG.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AWG.api.AppStartup
{
  public class Loader
  {
    private Loader() { }
    private readonly static Loader _current = new Loader();
    public static Loader Current { get { return _current; } }

    public List<string> Directories { get; private set; } = new List<string>();

    [ImportMany]
    public IEnumerable<IModule> Modules { get; private set; }

    public IEnumerable<Assembly> Assemblies { get; private set; }

    public void Compose()
    {
      // Catalogs does not exists in Dotnet Core, so you need to manage your own.
      var assemblies = new List<Assembly>() { typeof(Program).GetTypeInfo().Assembly };

      // All dlls in given directories
      foreach (var dir in this.Directories)
        assemblies.AddRange(Directory.GetFiles(dir, "*.dll", SearchOption.AllDirectories)
            .Select(AssemblyLoadContext.Default.LoadFromAssemblyPath)
            // Ensure that the assembly contains an implementation for the given type.
            .Where(s => s.GetTypes().Where(p => typeof(IModule).IsAssignableFrom(p)).Any()));

      this.Assemblies = assemblies;

      var configuration = new ContainerConfiguration().WithAssemblies(assemblies);
      using (var container = configuration.CreateContainer())
      {
        Modules = container.GetExports<IModule>();
      }

    }

    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
      foreach (var m in this.Modules)
        m.ConfigureServices(services, configuration);
    }
  }
}