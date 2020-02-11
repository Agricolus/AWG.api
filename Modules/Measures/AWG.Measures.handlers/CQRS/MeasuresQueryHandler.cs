using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using AWG.Measures.Core.Query;
using AWG.Measures.Core.Dto;
using AWG.Measures.Handlers.Model;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

using fiware = AWG.FIWARE.DataModels;

namespace AWG.Measures.Handlers.Query
{
  public class MeasuresQueryHandler : IRequestHandler<GetLastMeasure, fiware.WeatherObserved>,
                                      IRequestHandler<GetMeasuresList, IEnumerable<fiware.WeatherObserved>>,
                                      IRequestHandler<GetDailyMeasures, IEnumerable<DailyMeasureDetail>>,
                                      IRequestHandler<GetWeeklyMeasures, IEnumerable<WeeklyMeasureDetail>>,
                                      IRequestHandler<GetMonthlyMeasures, IEnumerable<MonthlyMeasureDetail>>
  {
    private MeasuresContext db;
    private readonly IMediator mediator;

    public MeasuresQueryHandler(MeasuresContext db, IMediator mediator)
    {
      this.db = db;
      this.mediator = mediator;
    }

    public async Task<fiware.WeatherObserved> Handle(GetLastMeasure request, CancellationToken cancellationToken)
    {
      var result = await (from m in db.WeatherMeasures
                          where m.RefDevice == request.StationId
                          orderby m.DateObserved descending
                          select m).Take(1).FirstOrDefaultAsync();

      return Mapper.Map<fiware.WeatherObserved>(result);
    }

    public async Task<IEnumerable<fiware.WeatherObserved>> Handle(GetMeasuresList request, CancellationToken cancellationToken)
    {
      var result = await (from m in db.WeatherMeasures
                          where m.DateObserved >= request.FromDate &&
                                m.DateObserved < request.ToDate &&
                                m.RefDevice == request.StationId
                          select m).ProjectTo<fiware.WeatherObserved>().ToListAsync();

      return result;
    }

    public async Task<IEnumerable<DailyMeasureDetail>> Handle(GetDailyMeasures request, CancellationToken cancellationToken)
    {
      var fromdate = request.FromDate.Date;
      DateTime? todate = null;

      if (request.ToDate != null)
        todate = request.ToDate.Value.Date.AddDays(1);

      var result = await (from m in db.WeatherMeasures
                          where m.DateObserved >= fromdate &&
                                (todate == null || m.DateObserved < todate) &&
                                m.RefDevice == request.StationId
                          group m by new { m.RefDevice, Date = m.DateObserved.Date } into gg
                          select new DailyMeasureDetail()
                          {
                            StationId = gg.Key.RefDevice,
                            Date = gg.Key.Date,
                            AvgRelativeHumidity = gg.Average(g => g.RelativeHumidity),
                            MinRelativeHumidity = gg.Min(g => g.RelativeHumidity),
                            MaxRelativeHumidity = gg.Max(g => g.RelativeHumidity),
                            AvgSolarRadiations = gg.Average(g => g.SolarRadiation),
                            AvgTemperature = gg.Average(g => g.Temperature),
                            AvgWindSpeed = gg.Average(g => g.WindSpeed),
                            MaxTemperature = gg.Max(g => g.Temperature),
                            MinTemperature = gg.Min(g => g.Temperature),
                            MaxWindSpeed = gg.Max(g => g.WindSpeed),
                            MinWindSpeed = gg.Min(g => g.WindSpeed),
                            Precipitations = gg.Sum(g => g.Precipitation),
                            SolarRadiations = gg.Sum(g => g.SolarRadiation),
                          }).ToListAsync();

      return result;
    }

    public async Task<IEnumerable<WeeklyMeasureDetail>> Handle(GetWeeklyMeasures request, CancellationToken cancellationToken)
    {
      var fromdate = request.FromDate.Date;
      DateTime? todate = null;

      if (request.ToDate != null)
        todate = request.ToDate.Value.Date.AddDays(1);

      var result = await (from m in db.WeatherMeasures
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
                            AvgRelativeHumidity = gg.Average(g => g.RelativeHumidity),
                            MinRelativeHumidity = gg.Min(g => g.RelativeHumidity),
                            MaxRelativeHumidity = gg.Max(g => g.RelativeHumidity),
                            AvgSolarRadiations = gg.Average(g => g.SolarRadiation),
                            AvgTemperature = gg.Average(g => g.Temperature),
                            AvgWindSpeed = gg.Average(g => g.WindSpeed),
                            MaxTemperature = gg.Max(g => g.Temperature),
                            MinTemperature = gg.Min(g => g.Temperature),
                            MaxWindSpeed = gg.Max(g => g.WindSpeed),
                            MinWindSpeed = gg.Min(g => g.WindSpeed),
                            Precipitations = gg.Sum(g => g.Precipitation),
                            SolarRadiations = gg.Sum(g => g.SolarRadiation),
                          }).OrderBy(m => m.Date).ToListAsync();

      return result;
    }

    public async Task<IEnumerable<MonthlyMeasureDetail>> Handle(GetMonthlyMeasures request, CancellationToken cancellationToken)
    {
      var fromdate = request.FromDate.Date;
      DateTime? todate = null;

      if (request.ToDate != null)
        todate = request.ToDate.Value.Date.AddDays(1);

      var result = await (from m in db.WeatherMeasures
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
                            AvgRelativeHumidity = gg.Average(g => g.RelativeHumidity),
                            MinRelativeHumidity = gg.Min(g => g.RelativeHumidity),
                            MaxRelativeHumidity = gg.Max(g => g.RelativeHumidity),
                            AvgSolarRadiations = gg.Average(g => g.SolarRadiation),
                            AvgTemperature = gg.Average(g => g.Temperature),
                            AvgWindSpeed = gg.Average(g => g.WindSpeed),
                            MaxTemperature = gg.Max(g => g.Temperature),
                            MinTemperature = gg.Min(g => g.Temperature),
                            MaxWindSpeed = gg.Max(g => g.WindSpeed),
                            MinWindSpeed = gg.Min(g => g.WindSpeed),
                            Precipitations = gg.Sum(g => g.Precipitation),
                            SolarRadiations = gg.Sum(g => g.SolarRadiation),
                          }).ToListAsync();

      return result;
    }
  }
}