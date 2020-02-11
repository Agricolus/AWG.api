using System;
using AWG.Measures.Dto;

namespace AWG.Measures.core.Dto
{
  public class MeasureData
  {
    public string StationId { get; set; }
    public DateTime Date { get; set; }
    public SensorDataRaw[] Datas { get; set; }
  }
}