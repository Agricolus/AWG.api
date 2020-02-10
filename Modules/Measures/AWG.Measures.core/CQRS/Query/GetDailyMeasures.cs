using MediatR;
using System;
using System.Collections.Generic;

namespace AWG.Measures.Core.Query
{
  public class GetDailyMeasures : IRequest<IEnumerable<AWG.Measures.Core.Dto.DailyMeasureDetail>>
  {
    public string StationId { get; set; }
    public DateTime FromDate { get; set; }
    public DateTime? ToDate { get; set; }
  }
}