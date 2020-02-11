using MediatR;
using fiware = AWG.FIWARE.DataModels;

namespace AWG.Measures.core.Query
{
  public class GetLastMeasure : IRequest<fiware.WeatherObserved>
  {
    public string StationId { get; set; }
  }
}