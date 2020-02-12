using System;
using MediatR;
using AWG.Measures.handlers.Model;
using System.Threading.Tasks;
using System.Threading;
using AutoMapper;
using System.Linq;
using AWG.Measures.core.Command;
using Microsoft.EntityFrameworkCore;
using BAMCIS.GeoJSON;
using System.Net.Http;
using Newtonsoft.Json;

namespace AWG.Measures.handlers.Command
{
  public class MeasuresNotificationCommandHandler : INotificationHandler<UpdateMeasureLocation>
  {
    private readonly MeasuresContext db;
    private readonly IMediator mediator;
    private readonly IMapper mapper;

    public MeasuresNotificationCommandHandler(MeasuresContext db, IMediator mediator, IMapper mapper)
    {
      this.db = db;
      this.mediator = mediator;
      this.mapper = mapper;
    }

    public async Task Handle(UpdateMeasureLocation request, CancellationToken cancellationToken)
    {
      var measure = await db.WeatherMeasures.Where(f => f.Id == request.Id).FirstOrDefaultAsync();

      if (measure == null)
        return;

      if (measure.Location != null)
      {
        if (measure.Location.Type == GeoJsonType.Point)
        {
          var point = (Point)measure.Location;

          measure.Latitude = point.Coordinates.Latitude;
          measure.Longitude = point.Coordinates.Longitude;
        }
        else
        {
          // OTHER GEOMETRIES
        }

        await db.SaveChangesAsync();

        return;
      }

      // GEOCODING
      if (measure.Address != null)
      {
        try
        {
          var baseUrl = $"https://nominatim.openstreetmap.org/?limit=1&format=json&q={measure.Address}";

          using (HttpClient client = new HttpClient())

          using (HttpResponseMessage res = await client.GetAsync(baseUrl))
          using (HttpContent content = res.Content)
          {
            string data = await content.ReadAsStringAsync();


            await db.SaveChangesAsync();
          }
        }
        catch (Exception e)
        {
          Console.WriteLine(e.StackTrace);
        }
      }
    }
  }
}