using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System.Composition;

namespace AWG.Common
{

  public interface IModule
  {

    void ConfigureServices(IServiceCollection services, IConfiguration configuration);

  }
}