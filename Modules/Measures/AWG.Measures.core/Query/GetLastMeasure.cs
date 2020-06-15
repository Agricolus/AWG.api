using MediatR;
using AWG.Measures.core.Dto;

namespace AWG.Measures.core.Query
{
  public class GetLastMeasure : IRequest<MeasureDetail>
  {
    public string StationId { get; set; }
  }
}