using System.Composition;
using AWG.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AWG.Measures.handlers.Model;

namespace AWG.Measures.handlers
{
  [Export(typeof(IModule))]
  public class Module : IModule
  {

    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
      services.AddDbContext<MeasuresContext>(options => options.UseNpgsql(configuration.GetConnectionString("AWGContext")));
    }
  }
}