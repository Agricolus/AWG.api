using System.Composition;
using AWG.Common;
using AWG.Measures.handlers.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AWG.Measures.handlers
{
  [Export(typeof(IModule))]
  public class Module : IModule
  {

    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
      services.AddDbContext<MeasturesContext>(options => options.UseNpgsql(configuration.GetConnectionString("AWGContext")));
    }
  }
}