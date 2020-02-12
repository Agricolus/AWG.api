using MediatR;
using AWG.Stations.handlers.Model;
using System.Threading.Tasks;
using System.Threading;
using AutoMapper;
using System.Linq;
using AWG.Stations.core.Command;
using Microsoft.EntityFrameworkCore;
using BAMCIS.GeoJSON;

namespace AWG.Stations.handlers.Command
{
  public class StationsNotificationCommandHandler : INotificationHandler<UpdateStationLocation>
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

    public async Task Handle(UpdateStationLocation request, CancellationToken cancellationToken)
    {
      var station = await db.Stations.Where(f => f.Id == request.Id).FirstOrDefaultAsync();

      if (station == null || station.Location == null)
        return;

      if (station.Location.Type == GeoJsonType.Point)
      {
        var point = (Point)station.Location;

        station.Latitude = point.Coordinates.Latitude;
        station.Longitude = point.Coordinates.Longitude;
      }
      else
      {
        // OTHER GEOMETRIES
      }

      await db.SaveChangesAsync();
    }
  }
}