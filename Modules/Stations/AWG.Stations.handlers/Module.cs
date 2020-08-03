using System.Composition;
using AWG.Common;
using AWG.Stations.handlers.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;

namespace AWG.Stations.handlers
{
  [Export(typeof(IModule))]
  public class Module : IModule
  {
    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
      Console.WriteLine($"AWG.Stations.handlers: module service configuration\n\tconnection string: {configuration.GetConnectionString("AWGPostgreContext")}");
      switch (configuration["DataBaseType"])
      {
        case "postgre":
          services.AddDbContext<StationsContext>(
            options => options.UseNpgsql(
              configuration.GetConnectionString("AWGPostgreContext"),
              providerOptions => providerOptions.EnableRetryOnFailure()
            )
          );
          services.AddDbContext<PostgresContext>();
          break;
        case "mysql":
          services.AddDbContext<StationsContext>(
            options => options.UseMySql(
              configuration.GetConnectionString("AWGMySqlContext"),
              providerOptions => providerOptions.EnableRetryOnFailure()
            )
          );
          services.AddDbContext<MySqlContext>();
          break;
      }

      services.AddMediatR(Assembly.GetExecutingAssembly());
    }

    public void Configure(IApplicationBuilder app, IHostEnvironment env)
    {
      using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
      {
        using (var context = serviceScope.ServiceProvider.GetService<PostgresContext>())
        {
          if (context == null) return;
          Console.WriteLine("AWG.Stations.handlers: migrations");
          Console.WriteLine("\tapplied:" + string.Join(',', context.Database.GetMigrations()));
          Console.WriteLine("\tpending:" + string.Join(',', context.Database.GetPendingMigrations()));
          context.Database.Migrate();
        }
        using (var context = serviceScope.ServiceProvider.GetService<MySqlContext>())
        {
          if (context == null) return;
          Console.WriteLine("AWG.Stations.handlers: migrations");
          Console.WriteLine("\tapplied:" + string.Join(',', context.Database.GetMigrations()));
          Console.WriteLine("\tpending:" + string.Join(',', context.Database.GetPendingMigrations()));
          context.Database.Migrate();
        }
      }
    }
  }
}
