using AWG.Measures.Dto;
using MediatR;
using System;

namespace AWG.Measures.core.Command
{
  public class AddMeasure : IRequest
  {
    public string StationId { get; set; }
    public DateTime Date { get; set; }
    public SensorDataRaw[] Data { get; set; }
  }
}