using MediatR;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using AWG.Measures.Handlers.Model;
using AWG.Measures.Core.Command;
using AWG.FIWARE.DataModels;

namespace AWG.Measures.Handlers.Command
{
  public class MeasuresCommandHandler : IRequestHandler<AddMeasure>
  {
    private MeasuresContext db;
    private readonly IMediator mediator;

    public MeasuresCommandHandler(MeasuresContext db, IMediator mediator)
    {
      this.db = db;
      this.mediator = mediator;
    }

    public async Task<Unit> Handle(AddMeasure request, CancellationToken cancellationToken)
    {
      var measure = db.WeatherMeasures.Where(m => m.RefDevice == request.StationId && m.DateObserved == request.Date).FirstOrDefault();

      if (measure == null)
      {
        measure = new WeatherMeasure()
        {
          //Id = ,
          //DataProvider = ,
          DateModified = DateTime.UtcNow,
          DateCreated = DateTime.UtcNow,
          //Name = ,
          //Address = ,
          DateObserved = request.Date,
          //Source = ,
          RefDevice = request.StationId,
          //RefPointOfInterest = ,
        };

        db.WeatherMeasures.Add(measure);

        //station.LastSyncronizationDate = request.Date;
      }
      else
      {
        measure.DateObserved = DateTime.UtcNow;

        //station.LastSyncronizationDate = request.Date;
      }

      //station.NextAllowedSyncronizationDate = DateTime.UtcNow.AddMinutes(10);

      foreach (var d in request.Data)
      {
        if (d.SensorName == nameof(measure.WeatherType))
        {
          measure.WeatherType = (WeatherTypeEnum)d.Value;
        }

        if (d.SensorName == nameof(measure.DewPoint))
        {
          measure.DewPoint = d.Value;
        }

        if (d.SensorName == nameof(measure.Visibility))
        {
          measure.Visibility = (WeatherMeasureVisibilityEnum)d.Value;
        }

        if (d.SensorName == nameof(measure.Temperature))
        {
          measure.Temperature = d.Value;
        }

        if (d.SensorName == nameof(measure.RelativeHumidity))
        {
          measure.RelativeHumidity = d.Value;
        }

        if (d.SensorName == nameof(measure.Precipitation))
        {
          measure.Precipitation = d.Value;
        }

        if (d.SensorName == nameof(measure.WindDirection))
        {
          measure.WindDirection = d.Value;
        }

        if (d.SensorName == nameof(measure.WindSpeed))
        {
          measure.WindSpeed = d.Value;
        }

        if (d.SensorName == nameof(measure.AtmosphericPressure))
        {
          measure.AtmosphericPressure = d.Value;
        }

        if (d.SensorName == nameof(measure.PressureTendency))
        {
          measure.PressureTendency = (PressureTendencyEnum)d.Value;
        }

        if (d.SensorName == nameof(measure.SolarRadiation))
        {
          measure.SolarRadiation = d.Value;
        }

        if (d.SensorName == nameof(measure.Illuminance))
        {
          measure.Illuminance = d.Value;
        }

        if (d.SensorName == nameof(measure.StreamGauge))
        {
          measure.StreamGauge = d.Value;
        }

        if (d.SensorName == nameof(measure.SnowHeight))
        {
          measure.SnowHeight = d.Value;
        }
      }

      await db.SaveChangesAsync();

      return Unit.Value;
    }
  }
}
