using MediatR;
using AWG.Stations.handlers.Model;
using System.Threading.Tasks;
using System.Threading;
using AutoMapper;
using System.Linq;
using AWG.Stations.core.Command;
using Microsoft.EntityFrameworkCore;
using BAMCIS.GeoJSON;
using AWG.Measures.core.Command;

namespace AWG.Stations.handlers.Command
{
  public class StationsNotificationCommandHandler : INotificationHandler<UpdateStationNotification>,
                                                    INotificationHandler<UpdateMeasureNotification>
  {
    private readonly StationsContext db;
    private readonly IMediator mediator;
    private readonly IMapper mapper;

    public StationsNotificationCommandHandler(StationsContext db, IMediator mediator, IMapper mapper)
    {
      this.db = db;
      this.mediator = mediator;
      this.mapper = mapper;
    }

    public async Task Handle(UpdateStationNotification request, CancellationToken cancellationToken)
    {
      var station = await db.Stations.Where(f => f.Id == request.Id).FirstOrDefaultAsync();

      if (station == null) return;

      if (station.Location != null)
        UpdateLocation(station);

      await db.SaveChangesAsync();
    }

    public async Task Handle(UpdateMeasureNotification notification, CancellationToken cancellationToken)
    {
      var station = await db.Stations.Where(f => f.Id == notification.Measure.RefDevice).FirstOrDefaultAsync();

      if (station == null) return;

      if (station.DateLastValueReported == null || station.DateLastValueReported < notification.Measure.DateObserved)
        station.DateLastValueReported = notification.Measure.DateObserved;

      await db.SaveChangesAsync();
    }

    private void UpdateLocation(Station station)
    {
      if (station.Location != null)
      {
        switch (station.Location.Type)
        {
          case GeoJsonType.Point:
            var point = (Point)station.Location;
            station.Latitude = point.Coordinates.Latitude;
            station.Longitude = point.Coordinates.Longitude;
            break;
          default:
            if (station.Location.BoundingBox != null)
            {
              station.Longitude = station.Location.BoundingBox.Where(i => i % 2 == 0).Average();
              station.Latitude = station.Location.BoundingBox.Where(i => i % 2 == 1).Average();
            }
            break;
        }
      }
    }
  }
}