using MediatR;

namespace AWG.Measures.Core.Query
{
  public class GetLastMeasure : IRequest<AWG.Measures.Core.Dto.MeasureDetail>
  {
    public string StationId { get; set; }
  }
}