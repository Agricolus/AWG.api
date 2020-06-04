
using System.Runtime.InteropServices;
using MediatR;
using AWG.Stations.handlers.Model;
using System.Threading.Tasks;
using System.Threading;
using AWG.Stations.core.Command;
using AWG.Stations.handlers.Helpers;
using System;
using Microsoft.Extensions.Configuration;

namespace AWG.Stations.handlers.Command
{
  public class StationCBCommandHandler : IRequestHandler<SubscribeStation, string>, IRequestHandler<UnSubscribeStation>
  {
    private readonly StationsContext db;
    private readonly IConfiguration configuration;

    public StationCBCommandHandler(StationsContext db, IConfiguration configuration)
    {
      this.db = db;
      this.configuration = configuration;
    }

    public async Task<string> Handle(SubscribeStation request, CancellationToken cancellationToken)
    {
      var contextBorkerUrl = this.configuration["ContextBroker:url"];
      var contextBrokerNotificationEndpoint = this.configuration["ContextBroker:notificationEndpoint"];
      var cbclinet = new ContextBrokerClient(contextBorkerUrl);

      //TODO controllare se non esiste già
      var sub = new Subscription()
      {
        Description = $"station subscription - idStation: {request.Id}",
        Subject = new Subject()
        {
          Entities = new Entity[] {
            new Entity{
              Id = request.Id,
              Type = "Device"
            }
          }
        },
        Notification = new Notification()
        {
          Http = new Http()
          {
            Url = new Uri(string.Format(contextBrokerNotificationEndpoint, request.Id))
          }
        }
      };
      try
      {
        return await cbclinet.CreateSubscritpion(sub);
      }
      catch
      {
        return null;
      }
    }

    public async Task<Unit> Handle(UnSubscribeStation request, CancellationToken cancellationToken)
    {

      var contextBorkerUrl = this.configuration["ContextBroker:url"];
      var contextBrokerNotificationEndpoint = this.configuration["ContextBroker:notificationEndpoint"];
      var cbclinet = new ContextBrokerClient(contextBorkerUrl);


      var subscription = await cbclinet.GetSubscritpion(request.subId);
      if (subscription != null)
        await cbclinet.DeleteSubscritpion(request.subId);

      return Unit.Value;
    }
  }

}