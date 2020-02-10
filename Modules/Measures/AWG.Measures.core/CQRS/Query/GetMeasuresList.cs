using MediatR;
using System;
using System.Collections.Generic;

namespace AWG.Measures.Core.Query
{
  public class GetMeasuresList : IRequest<IEnumerable<AWG.Measures.Core.Dto.MeasureDetail>>
  {
    public string StationId { get; set; }
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
  }
}