using System;
using AWG.Measures.Dto;

namespace AWG.Measures.Core.Dto
{
  public class MeasureData
  {
    public string StationId { get; set; }
    public DateTime Date { get; set; }
    public SensorDataRaw[] Datas { get; set; }
  }
}