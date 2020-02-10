using Microsoft.Extensions.DependencyInjection;
using System.Composition;

namespace AWG.Common
{

  public interface IModule
  {

    void ConfigureServices(IServiceCollection services);

  }
}