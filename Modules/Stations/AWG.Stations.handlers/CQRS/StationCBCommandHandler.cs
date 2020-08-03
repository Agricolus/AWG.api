
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
using AWG.Stations.core.dto;

namespace AWG.Stations.handlers.Command
{
  public class StationCBCommandHandler : IRequestHandler<CreateOrUpdateCBEntity, string>,
                                         IRequestHandler<SubscribeToCBEntity, string>,
                                         IRequestHandler<UnsubscribeFromCBEntity>
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

    public async Task<string> Handle(CreateOrUpdateCBEntity request, CancellationToken cancellationToken)
    {
      var contextBorkerUrl = this.configuration["ContextBroker:Url"];
      var fiwareService = this.configuration["FiwareServices:Service"];
      var fiwareServicePath = this.configuration["FiwareServices:ServicePath"];

      var cbclient = new ContextBrokerClient(fiwareService, fiwareServicePath, contextBorkerUrl);

      var station = await mediator.Send(new GetStation() { Id = request.Id });
      var entityId = $"urn:ngsi-ld:WeatherObserved:{station.DataProvider}-{station.SerialNumber}";
      var device = await cbclient.RetrieveEntity<WeatherObservedCBEntity>(entityId, "WeatherObserved");

      if (device == null)
      {
        device = new WeatherObservedCBEntity()
        {
          Id = entityId,
          RefDevice = station.Id,
          DateCreated = station.DateCreated.Value.ToString("u"),
          DateModified = station.DateModified.Value.ToString("u"),
          DataProvider = station.DataProvider,
          DateObserved = null,
          Source = station.Source,
          Name = station.Name,
          Location = station.Location
        };

        await cbclient.CreateEntity<WeatherObservedCBEntity>(device, AttributesFormatEnum.keyValues);
      }
      else
      {
        // To update the entity I must have an object without id and type properties
        // It must contains only the properties to updated
        // https://fiware-orion.readthedocs.io/en/1.8.0/user/walkthrough_apiv2/index.html#update-entity
        var entity = new WeatherObservedCBEntityUpdate()
        {
          RefDevice = station.Id,
          DateCreated = station.DateCreated.Value.ToString("u"),
          DateModified = station.DateModified.Value.ToString("u"),
          DataProvider = station.DataProvider,
          Source = station.Source,
          Name = station.Name,
          Location = station.Location
        };

        await cbclient.UpdateEntity<WeatherObservedCBEntityUpdate>(entityId, entity, "WeatherObserved", AttributesFormatEnum.keyValues);
      }

      return device.Id;
    }

    public async Task<string> Handle(SubscribeToCBEntity request, CancellationToken cancellationToken)
    {
      var contextBorkerUrl = this.configuration["ContextBroker:Url"];
      var fiwareService = this.configuration["FiwareServices:Service"];
      var fiwareServicePath = this.configuration["FiwareServices:ServicePath"];

      var cbclient = new ContextBrokerClient(fiwareService, fiwareServicePath, contextBorkerUrl);

      var device = await cbclient.RetrieveEntity<fiware.WeatherObserved>(request.Id, "WeatherObserved");
      if (device == null)
        return null;

      var contextBrokerNotificationEndpoint = this.configuration["ContextBroker:NotificationEndpoint"];

      var sub = new Subscription()
      {
        Subject = new Subject()
        {
          Entities = new Entity[] {
              new Entity {
                Id = request.Id,
                Type = "WeatherObserved"
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

    public async Task<Unit> Handle(UnsubscribeFromCBEntity request, CancellationToken cancellationToken)
    {
      if (String.IsNullOrEmpty(request.SubscriptionId))
        return Unit.Value;

      var contextBorkerUrl = this.configuration["ContextBroker:Url"];
      var fiwareService = this.configuration["FiwareServices:Service"];
      var fiwareServicePath = this.configuration["FiwareServices:ServicePath"];

      var cbclient = new ContextBrokerClient(fiwareService, fiwareServicePath, contextBorkerUrl);

      var subscription = await cbclient.GetSubscription(request.SubscriptionId);

      if (subscription != null)
        await cbclient.DeleteSubscription(request.SubscriptionId);

      return Unit.Value;
    }
  }
}