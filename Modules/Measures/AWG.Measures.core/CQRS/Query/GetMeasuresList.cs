using MediatR;
using System;
using System.Collections.Generic;
using fiware = AWG.FIWARE.DataModels;

namespace AWG.Measures.Core.Query
{
  public class GetMeasuresList : IRequest<IEnumerable<fiware.WeatherObserved>>
  {
    public string StationId { get; set; }
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
  }
}