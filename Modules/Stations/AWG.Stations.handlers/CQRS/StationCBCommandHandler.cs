
using MediatR;
using AWG.Stations.handlers.Model;
using System.Threading.Tasks;
using System.Threading;
using AWG.Stations.core.Command;
using System;
using Microsoft.Extensions.Configuration;
using AWG.Common.Helpers;
using fiware = AWG.FIWARE.DataModels;
using AWG.Common.Enums;
using AWG.Stations.core.Query;

namespace AWG.Stations.handlers.Command
{
  public class StationCBCommandHandler : IRequestHandler<SubscribeStation, string>, IRequestHandler<UnsubscribeStation>
  {
    private readonly StationsContext db;
    private readonly IConfiguration configuration;
    private readonly IMediator mediator;

    public StationCBCommandHandler(StationsContext db, IConfiguration configuration, IMediator mediator)
    {
      this.db = db;
      this.configuration = configuration;
      this.mediator = mediator;
    }

    public async Task<string> Handle(SubscribeStation request, CancellationToken cancellationToken)
    {
      var contextBorkerUrl = this.configuration["ContextBroker:url"];
      var contextBrokerNotificationEndpoint = this.configuration["ContextBroker:notificationEndpoint"];
      var cbclient = new ContextBrokerClient(contextBorkerUrl);

      var device = await cbclient.RetrieveEntity<fiware.Device>(request.Id, "Device");

      if (device == null)
      {
        device = await mediator.Send(new GetStation() { Id = request.Id });

        await cbclient.CreateEntity<fiware.Device>(device, AttributesFormatEnum.keyValues);
      }

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

      return await cbclient.CreateSubscription(sub);
    }

    public async Task<Unit> Handle(UnsubscribeStation request, CancellationToken cancellationToken)
    {
      var contextBorkerUrl = this.configuration["ContextBroker:url"];
      var cbclient = new ContextBrokerClient(contextBorkerUrl);

      var subscription = await cbclient.GetSubscription(request.SubscriptionId);

      if (subscription != null)
        await cbclient.DeleteSubscription(request.SubscriptionId);

      return Unit.Value;
    }
  }
}