using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using AWG.Measures.core.Query;
using AWG.Measures.core.Dto;
using AWG.Measures.handlers.Model;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using fiware = AWG.FIWARE.DataModels;

namespace AWG.Measures.handlers.Query
{
  public class MeasuresQueryHandler : IRequestHandler<GetLastMeasure, fiware.WeatherObserved>,
                                      IRequestHandler<GetMeasuresList, IEnumerable<fiware.WeatherObserved>>,
                                      IRequestHandler<GetDailyMeasures, IEnumerable<DailyMeasureDetail>>,
                                      IRequestHandler<GetWeeklyMeasures, IEnumerable<WeeklyMeasureDetail>>,
                                      IRequestHandler<GetMonthlyMeasures, IEnumerable<MonthlyMeasureDetail>>
  {
    private readonly MeasuresContext db;
    private readonly IMediator mediator;
    private readonly IMapper mapper;

    public MeasuresQueryHandler(MeasuresContext db, IMediator mediator, IMapper mapper)
    {
      this.db = db;
      this.mediator = mediator;
      this.mapper = mapper;
    }

    public async Task<fiware.WeatherObserved> Handle(GetLastMeasure request, CancellationToken cancellationToken)
    {
      var result = await (from m in db.WeatherMeasures
                          where m.RefDevice == request.StationId
                          orderby m.DateObserved descending
                          select m).Take(1).FirstOrDefaultAsync();

      return mapper.Map<fiware.WeatherObserved>(result);
    }

    public async Task<IEnumerable<fiware.WeatherObserved>> Handle(GetMeasuresList request, CancellationToken cancellationToken)
    {
      return await (from m in db.WeatherMeasures
                    where m.DateObserved >= request.FromDate &&
                          m.DateObserved < request.ToDate &&
                          m.RefDevice == request.StationId
                    select m).ProjectTo<fiware.WeatherObserved>(mapper.ConfigurationProvider).ToListAsync();
    }

    public async Task<IEnumerable<DailyMeasureDetail>> Handle(GetDailyMeasures request, CancellationToken cancellationToken)
    {
      var fromdate = request.FromDate.Date;
      DateTime? todate = null;

      if (request.ToDate != null)
        todate = request.ToDate.Value.Date.AddDays(1);

      return await (from m in db.WeatherMeasures
                    where m.DateObserved >= fromdate &&
                          (todate == null || m.DateObserved < todate) &&
                          m.RefDevice == request.StationId
                    group m by new { m.RefDevice, Date = m.DateObserved.Date } into gg
                    select new DailyMeasureDetail()
                    {
                      StationId = gg.Key.RefDevice,
                      Date = gg.Key.Date,
                      Precipitations = gg.Sum(g => g.Precipitation),
                      AvgRelativeHumidity = gg.Average(g => g.RelativeHumidity),
                      MinRelativeHumidity = gg.Min(g => g.RelativeHumidity),
                      MaxRelativeHumidity = gg.Max(g => g.RelativeHumidity),
                      AvgSolarRadiations = gg.Average(g => g.SolarRadiation),
                      MinSolarRadiations = gg.Min(g => g.SolarRadiation),
                      MaxSolarRadiations = gg.Max(g => g.SolarRadiation),
                      AvgTemperature = gg.Average(g => g.Temperature),
                      MinTemperature = gg.Min(g => g.Temperature),
                      MaxTemperature = gg.Max(g => g.Temperature),
                      AvgWindSpeed = gg.Average(g => g.WindSpeed),
                      MinWindSpeed = gg.Min(g => g.WindSpeed),
                      MaxWindSpeed = gg.Max(g => g.WindSpeed),
                      AvgWindDirection = gg.Average(g => g.WindDirection),
                      MinWindDirection = gg.Min(g => g.WindDirection),
                      MaxWindDirection = gg.Max(g => g.WindDirection),
                      AvgAtmosphericPressure = gg.Average(g => g.AtmosphericPressure),
                      MinAtmosphericPressure = gg.Min(g => g.AtmosphericPressure),
                      MaxAtmosphericPressure = gg.Max(g => g.AtmosphericPressure),
                      AvgDewPoint = gg.Average(g => g.DewPoint),
                      MinDewPoint = gg.Min(g => g.DewPoint),
                      MaxDewPoint = gg.Max(g => g.DewPoint),
                      AvgIlluminance = gg.Average(g => g.Illuminance),
                      MinIlluminance = gg.Min(g => g.Illuminance),
                      MaxIlluminance = gg.Max(g => g.Illuminance),
                      AvgStreamGauge = gg.Average(g => g.StreamGauge),
                      MinStreamGauge = gg.Min(g => g.StreamGauge),
                      MaxStreamGauge = gg.Max(g => g.StreamGauge),
                      AvgSnowHeight = gg.Average(g => g.SnowHeight),
                      MinSnowHeight = gg.Min(g => g.SnowHeight),
                      MaxSnowHeight = gg.Max(g => g.SnowHeight),
                    }).ToListAsync();
    }

    public async Task<IEnumerable<WeeklyMeasureDetail>> Handle(GetWeeklyMeasures request, CancellationToken cancellationToken)
    {
      var fromdate = request.FromDate.Date;
      DateTime? todate = null;

      if (request.ToDate != null)
        todate = request.ToDate.Value.Date.AddDays(1);

      return await (from m in db.WeatherMeasures
                    where m.DateObserved >= fromdate &&
                          (todate == null || m.DateObserved < todate) &&
                          m.RefDevice == request.StationId
                    group m by new { m.RefDevice, Year = m.DateObserved.Year, Week = 1 + (m.DateObserved.DayOfYear - 1) / 7 } into gg
                    select new WeeklyMeasureDetail()
                    {
                      StationId = gg.Key.RefDevice,
                      Year = gg.Key.Year,
                      Week = gg.Key.Week,
                      Date = gg.Min(g => g.DateObserved),
                      DateLast = gg.Max(g => g.DateObserved),
                      Precipitations = gg.Sum(g => g.Precipitation),
                      AvgRelativeHumidity = gg.Average(g => g.RelativeHumidity),
                      MinRelativeHumidity = gg.Min(g => g.RelativeHumidity),
                      MaxRelativeHumidity = gg.Max(g => g.RelativeHumidity),
                      AvgSolarRadiations = gg.Average(g => g.SolarRadiation),
                      MinSolarRadiations = gg.Min(g => g.SolarRadiation),
                      MaxSolarRadiations = gg.Max(g => g.SolarRadiation),
                      AvgTemperature = gg.Average(g => g.Temperature),
                      MinTemperature = gg.Min(g => g.Temperature),
                      MaxTemperature = gg.Max(g => g.Temperature),
                      AvgWindSpeed = gg.Average(g => g.WindSpeed),
                      MinWindSpeed = gg.Min(g => g.WindSpeed),
                      MaxWindSpeed = gg.Max(g => g.WindSpeed),
                      AvgWindDirection = gg.Average(g => g.WindDirection),
                      MinWindDirection = gg.Min(g => g.WindDirection),
                      MaxWindDirection = gg.Max(g => g.WindDirection),
                      AvgAtmosphericPressure = gg.Average(g => g.AtmosphericPressure),
                      MinAtmosphericPressure = gg.Min(g => g.AtmosphericPressure),
                      MaxAtmosphericPressure = gg.Max(g => g.AtmosphericPressure),
                      AvgDewPoint = gg.Average(g => g.DewPoint),
                      MinDewPoint = gg.Min(g => g.DewPoint),
                      MaxDewPoint = gg.Max(g => g.DewPoint),
                      AvgIlluminance = gg.Average(g => g.Illuminance),
                      MinIlluminance = gg.Min(g => g.Illuminance),
                      MaxIlluminance = gg.Max(g => g.Illuminance),
                      AvgStreamGauge = gg.Average(g => g.StreamGauge),
                      MinStreamGauge = gg.Min(g => g.StreamGauge),
                      MaxStreamGauge = gg.Max(g => g.StreamGauge),
                      AvgSnowHeight = gg.Average(g => g.SnowHeight),
                      MinSnowHeight = gg.Min(g => g.SnowHeight),
                      MaxSnowHeight = gg.Max(g => g.SnowHeight),
                    }).OrderBy(m => m.Date).ToListAsync();
    }

    public async Task<IEnumerable<MonthlyMeasureDetail>> Handle(GetMonthlyMeasures request, CancellationToken cancellationToken)
    {
      var fromdate = request.FromDate.Date;
      DateTime? todate = null;

      if (request.ToDate != null)
        todate = request.ToDate.Value.Date.AddDays(1);

      return await (from m in db.WeatherMeasures
                    where m.DateObserved >= fromdate &&
                          (todate == null || m.DateObserved < todate) &&
                          m.RefDevice == request.StationId
                    group m by new { m.RefDevice, Year = m.DateObserved.Year, Month = m.DateObserved.Month } into gg
                    select new MonthlyMeasureDetail()
                    {
                      StationId = gg.Key.RefDevice,
                      Year = gg.Key.Year,
                      Month = gg.Key.Month,
                      Date = gg.Min(g => g.DateObserved),
                      Precipitations = gg.Sum(g => g.Precipitation),
                      AvgRelativeHumidity = gg.Average(g => g.RelativeHumidity),
                      MinRelativeHumidity = gg.Min(g => g.RelativeHumidity),
                      MaxRelativeHumidity = gg.Max(g => g.RelativeHumidity),
                      AvgSolarRadiations = gg.Average(g => g.SolarRadiation),
                      MinSolarRadiations = gg.Min(g => g.SolarRadiation),
                      MaxSolarRadiations = gg.Max(g => g.SolarRadiation),
                      AvgTemperature = gg.Average(g => g.Temperature),
                      MinTemperature = gg.Min(g => g.Temperature),
                      MaxTemperature = gg.Max(g => g.Temperature),
                      AvgWindSpeed = gg.Average(g => g.WindSpeed),
                      MinWindSpeed = gg.Min(g => g.WindSpeed),
                      MaxWindSpeed = gg.Max(g => g.WindSpeed),
                      AvgWindDirection = gg.Average(g => g.WindDirection),
                      MinWindDirection = gg.Min(g => g.WindDirection),
                      MaxWindDirection = gg.Max(g => g.WindDirection),
                      AvgAtmosphericPressure = gg.Average(g => g.AtmosphericPressure),
                      MinAtmosphericPressure = gg.Min(g => g.AtmosphericPressure),
                      MaxAtmosphericPressure = gg.Max(g => g.AtmosphericPressure),
                      AvgDewPoint = gg.Average(g => g.DewPoint),
                      MinDewPoint = gg.Min(g => g.DewPoint),
                      MaxDewPoint = gg.Max(g => g.DewPoint),
                      AvgIlluminance = gg.Average(g => g.Illuminance),
                      MinIlluminance = gg.Min(g => g.Illuminance),
                      MaxIlluminance = gg.Max(g => g.Illuminance),
                      AvgStreamGauge = gg.Average(g => g.StreamGauge),
                      MinStreamGauge = gg.Min(g => g.StreamGauge),
                      MaxStreamGauge = gg.Max(g => g.StreamGauge),
                      AvgSnowHeight = gg.Average(g => g.SnowHeight),
                      MinSnowHeight = gg.Min(g => g.SnowHeight),
                      MaxSnowHeight = gg.Max(g => g.SnowHeight),
                    }).ToListAsync();
    }
  }
}