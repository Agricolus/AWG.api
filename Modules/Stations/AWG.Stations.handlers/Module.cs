using System.Composition;
using AWG.Common;
using AWG.Stations.handlers.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using System.Reflection;

namespace AWG.Stations.handlers
{
  [Export(typeof(IModule))]
  public class Module : IModule
  {
    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
      services.AddDbContext<StationsContext>(options => options.UseNpgsql(configuration.GetConnectionString("AWGContext")));
      services.AddMediatR(Assembly.GetExecutingAssembly());
    }
  }
}