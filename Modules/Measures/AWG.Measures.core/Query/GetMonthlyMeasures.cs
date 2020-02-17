using MediatR;
using System;
using System.Collections.Generic;
using AWG.Measures.core.Dto;

namespace AWG.Measures.core.Query
{
  public class GetMonthlyMeasures : IRequest<IEnumerable<MonthlyMeasureDetail>>
  {
    public string StationId { get; set; }
    public DateTime FromDate { get; set; }
    public DateTime? ToDate { get; set; }
  }
}