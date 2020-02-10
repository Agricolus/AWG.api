using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AWG.Measures.Core.Query;
using AWG.Measures.Core.Dto;

namespace AWG.Measures.API.Controllers
{
  [ApiController]
  [Route("api/measures")]
  public class MeasuresController : ControllerBase
  {
    private readonly ILogger<MeasuresController> logger;
    private readonly IMediator mediator;

    public MeasuresController(ILogger<MeasuresController> logger, IMediator mediator)
    {
      this.logger = logger;
      this.mediator = mediator;
    }

    [HttpGet]
    public IActionResult Test()
    {
      return Ok();
    }

    [Route("last"), HttpGet]
    public async Task<MeasureDetail> GetLastMeasure(string stationId)
    {
      return await mediator.Send(new GetLastMeasure() { StationId = stationId });
    }

    [Route("daily"), HttpGet]
    public async Task<IEnumerable<DailyMeasureDetail>> GetLastDailyMeasure(string stationId, DateTime? fromDate = null, DateTime? toDate = null)
    {
      return await mediator.Send(new GetDailyMeasures()
      {
        StationId = stationId,
        FromDate = fromDate ?? DateTime.UtcNow,
        ToDate = toDate,
      });
    }

    [Route("weekly"), HttpGet]
    public async Task<IEnumerable<WeeklyMeasureDetail>> GetLastWeeklyMeasure(string stationId, DateTime? fromDate = null, DateTime? toDate = null)
    {
      return await mediator.Send(new GetWeeklyMeasures()
      {
        StationId = stationId,
        FromDate = fromDate ?? DateTime.UtcNow,
        ToDate = toDate,
      });
    }

    [Route("monthly"), HttpGet]
    public async Task<IEnumerable<MonthlyMeasureDetail>> GetLastMonthlyMeasure(string stationId, DateTime? fromDate = null, DateTime? toDate = null)
    {
      return await mediator.Send(new GetMonthlyMeasures()
      {
        StationId = stationId,
        FromDate = fromDate ?? DateTime.UtcNow,
        ToDate = toDate,
      });
    }

    [Route("interval"), HttpGet]
    public async Task<IEnumerable<MeasureDetail>> GetMeasureList(string stationId, DateTime fromDate, DateTime? toDate = null)
    {
      return await mediator.Send(new GetMeasuresList()
      {
        StationId = stationId,
        FromDate = fromDate,
        ToDate = toDate ?? DateTime.Now.AddDays(1).Date,
      });
    }
  }
}