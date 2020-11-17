using System.Reflection.Metadata;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MediatR.Pipeline;
using MediatR;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using AWG.api.AppStartup;
using AutoMapper;

namespace AWG.api
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
      Loader.Current.Directories.Add(Directory.GetCurrentDirectory());
      Loader.Current.Compose();
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {


      services.AddCors(options =>
      {
        options.AddDefaultPolicy(builder =>
        {
          builder.WithOrigins("*").WithMethods("*").WithHeaders("*");
        });
      });

      Loader.Current.ConfigureServices(services, this.Configuration);

      services.AddControllers().AddNewtonsoftJson(options =>
      {
        options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
      });

      services.AddAutoMapper(Loader.Current.Assemblies);

      services.AddSwaggerGen(g =>
      {
        g.SwaggerDoc("v1", new OpenApiInfo { Title = "AWG", Version = "v1" });
      });

      // mediatr configuration
      services.AddMediatR(Assembly.GetExecutingAssembly());

    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      Loader.Current.Configure(app, env);

      app.UseSwagger();

      // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
      // specifying the Swagger JSON endpoint.
      app.UseSwaggerUI(c =>
      {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Agro Weather Gateway v1");
      });

      app.UseCors();

      app.UseRouting();
      app.UseDefaultFiles();
      app.UseStaticFiles();

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
