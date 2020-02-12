using System.Composition;
using System.Reflection;
using AWG.Common;
using AWG.FIWARE.Serializers;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using fiware = AWG.FIWARE.DataModels;

namespace AWG.Stations.api
{
  [Export(typeof(IModule))]
  public class Module : IModule
  {
    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
      var mvcbuilder = services.AddControllers();
      mvcbuilder.PartManager.ApplicationParts.Add(new AssemblyPart(Assembly.GetExecutingAssembly()));
      mvcbuilder.AddNewtonsoftJson(options =>
      {
        options.SerializerSettings.Converters.Add(new FiwareNormalizedJsonConverter<fiware.DeviceLD>());
      });
    }
  }
}