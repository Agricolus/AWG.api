using System.Composition;
using System.Reflection;
using AWG.Common;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AWG.Stations.api
{
  [Export(typeof(IModule))]
  public class Module : IModule
  {
    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
      services.AddControllers().PartManager.ApplicationParts.Add(new AssemblyPart(Assembly.GetExecutingAssembly()));
    }
  }
}