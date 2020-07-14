using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System.Composition;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

namespace AWG.Common
{

  public interface IModule
  {
    void Configure(IApplicationBuilder app, IHostEnvironment env)
    {

    }

    void ConfigureServices(IServiceCollection services, IConfiguration configuration);

  }
}