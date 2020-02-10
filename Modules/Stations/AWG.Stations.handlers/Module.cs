using System.Composition;
using AWG.Common;
using Microsoft.Extensions.DependencyInjection;

namespace AWG.Stations.handlers
{
  [Export(typeof(IModule))]
  public class Module : IModule
  {
    public void ConfigureServices(IServiceCollection services)
    {

    }
  }
}