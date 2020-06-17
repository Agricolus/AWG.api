using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using AutoMapper;
using AWG.Measures.handlers.Model;
using AWG.Stations.handlers.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace AWG.Tests
{
  public static class Helper
  {
    public static IMediator BuildMediator()
    {
      var services = new ServiceCollection();
      var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

      services.AddSingleton<IConfiguration>(config);

      services.AddDbContext<StationsContext>(options => options.UseNpgsql(config.GetConnectionString("AWGPostgreContext")));
      services.AddDbContext<MeasuresContext>(options => options.UseNpgsql(config.GetConnectionString("AWGPostgreContext")));

      //services.AddAutoMapper(typeof(AWG.Stations.handlers.MappingProfile).GetTypeInfo().Assembly);
      services.AddAutoMapper(typeof(AWG.Measures.handlers.MappingProfile).GetTypeInfo().Assembly);

      foreach (var assemblyName in Assembly.GetExecutingAssembly().GetReferencedAssemblies())
      {
        services.AddMediatR(Assembly.Load(assemblyName));
      }

      services.AddScoped(typeof(IPipelineBehavior<,>), typeof(GenericPipelineBehavior<,>));

      return services.BuildServiceProvider().GetRequiredService<IMediator>();
    }

    public static dynamic Parameters(string fileParameter, [CallerMemberName] string function = "")
    {
      var paramsFile = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), $"data\\{fileParameter}"));
      return JsonConvert.DeserializeObject(paramsFile);
    }
  }
}