using System.Net;
using System.Net.Sockets;
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
using System.Text;
using AWG.FIWARE.DataModels;
using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;

namespace AWG.Measures.handlers.Command
{
  public class MeasuresNotificationCommandHandler : INotificationHandler<UpdateMeasureNotification>
  {
    private readonly MeasuresContext db;
    private readonly IMediator mediator;
    private readonly IMapper mapper;
    private readonly IConfiguration configuration;

    public MeasuresNotificationCommandHandler(MeasuresContext db, IMediator mediator, IMapper mapper, IConfiguration configuration)
    {
      this.db = db;
      this.mediator = mediator;
      this.mapper = mapper;
      this.configuration = configuration;
    }

    public async Task Handle(UpdateMeasureNotification request, CancellationToken cancellationToken)
    {
      var measure = await db.WeatherMeasures.Where(f => f.Id == request.Id).FirstOrDefaultAsync();

      if (measure == null) return;

      if (measure.Location != null)
        UpdateLocation(measure);
      else if (measure.Address != null)
        await ResolveAddress(measure);

      await db.SaveChangesAsync();
    }


    private void UpdateLocation(WeatherMeasure measure)
    {
      if (measure.Location != null)
        switch (measure.Location.Type)
        {
          case GeoJsonType.Point:
            var point = (Point)measure.Location;

            measure.Latitude = point.Coordinates.Latitude;
            measure.Longitude = point.Coordinates.Longitude;
            break;

          default:
            if (measure.Location.BoundingBox != null)
            {
              measure.Longitude = measure.Location.BoundingBox.Where(i => i % 2 == 0).Average();
              measure.Latitude = measure.Location.BoundingBox.Where(i => i % 2 == 1).Average();
            }
            break;
        }
    }

    public async Task ResolveAddress(WeatherMeasure measure)
    {
      if (this.configuration["OSMGeocoding:EnableGeocodingForMeasures"] != "true") return;

      // GEOCODING
      if (measure.Address != null)

        try
        {
          // var baseUrl = $"https://nominatim.openstreetmap.org/?limit=1&format=json&q={measure.Address}";

          var uri = new StringBuilder(this.configuration["OSMGeocoding:OpenStreetMapURI"]);
          uri.Append("/search?limit=1&format=geojson");

          if (measure.Address.AddressCountry != null) uri.Append($"&country={measure.Address.AddressCountry}");
          if (measure.Address.AddressLocality != null) uri.Append($"&city={measure.Address.AddressLocality}");
          if (measure.Address.AddressRegion != null) uri.Append($"&state={measure.Address.AddressRegion}");
          if (measure.Address.PostalCode != null) uri.Append($"&postalcode={measure.Address.PostalCode}");
          if (measure.Address.StreetAddress != null) uri.Append($"&street={measure.Address.StreetAddress}");


          using (HttpClient client = new HttpClient())
          {
            client.DefaultRequestHeaders.Add("Referer", "AWG");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            using (HttpResponseMessage res = await client.GetAsync(uri.ToString()))
            using (HttpContent content = res.Content)
            {
              string data = await content.ReadAsStringAsync();

              var geojson = JsonConvert.DeserializeObject<GeoJson>(data);
              measure.Location = geojson;
              UpdateLocation(measure);
            }
          }
        }
        catch (Exception e)
        {
          Console.WriteLine(e.Message);
          Console.WriteLine(e.StackTrace);
        }
    }
  }
}