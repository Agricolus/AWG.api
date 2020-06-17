using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AWG.Tests
{
  public static class Program
  {
    /*public static void Main(string[] args)
    {
      //var writer = new WrappingWriter(Console.Out);
      var mediator = BuildMediator();
      //await GetAllActiveStations(mediator);
      //return Runner.Run(mediator, writer, "ASP.NET Core DI");
    }

    private static IMediator BuildMediator()
    {
      var services = new IServiceCollection();

      //services.AddSingleton<TextWriter>(writer);

      services.AddMediatR(Assembly.GetExecutingAssembly());

      services.AddScoped(typeof(IPipelineBehavior<,>), typeof(GenericPipelineBehavior<,>));
      services.AddScoped(typeof(IRequestPreProcessor<>), typeof(GenericRequestPreProcessor<>));
      services.AddScoped(typeof(IRequestPostProcessor<,>), typeof(GenericRequestPostProcessor<,>));

      var provider = services.BuildServiceProvider();

      return provider.GetRequiredService<IMediator>();
    }*/
  }
}