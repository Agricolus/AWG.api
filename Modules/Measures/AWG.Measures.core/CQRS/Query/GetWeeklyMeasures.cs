using MediatR;
using System;
using System.Collections.Generic;

namespace AWG.Measures.Core.Query
{
  public class GetWeeklyMeasures : IRequest<IEnumerable<AWG.Measures.Core.Dto.WeeklyMeasureDetail>>
  {
    public string StationId { get; set; }
    public DateTime FromDate { get; set; }
    public DateTime? ToDate { get; set; }
  }
}