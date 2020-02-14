using System;

namespace AWG.Measures.core.Dto
{
  public class WeeklyMeasureDetail
  {
    public string StationId { get; set; }
    public int Year { get; set; }
    public int Week { get; set; }
    public DateTime Date { get; set; }
    public DateTime DateLast { get; set; }
    public double? Precipitations { get; set; }
    public double? AvgRelativeHumidity { get; set; }
    public double? MinRelativeHumidity { get; set; }
    public double? MaxRelativeHumidity { get; set; }
    public double? AvgSolarRadiations { get; set; }
    public double? MinSolarRadiations { get; set; }
    public double? MaxSolarRadiations { get; set; }
    public double? AvgTemperature { get; set; }
    public double? MinTemperature { get; set; }
    public double? MaxTemperature { get; set; }
    public double? AvgWindSpeed { get; set; }
    public double? MinWindSpeed { get; set; }
    public double? MaxWindSpeed { get; set; }
    public double? AvgWindDirection { get; set; }
    public double? MinWindDirection { get; set; }
    public double? MaxWindDirection { get; set; }
    public double? AvgAtmosphericPressure { get; set; }
    public double? MinAtmosphericPressure { get; set; }
    public double? MaxAtmosphericPressure { get; set; }
    public double? AvgDewPoint { get; set; }
    public double? MinDewPoint { get; set; }
    public double? MaxDewPoint { get; set; }
    public double? AvgIlluminance { get; set; }
    public double? MinIlluminance { get; set; }
    public double? MaxIlluminance { get; set; }
    public double? AvgStreamGauge { get; set; }
    public double? MinStreamGauge { get; set; }
    public double? MaxStreamGauge { get; set; }
    public double? AvgSnowHeight { get; set; }
    public double? MinSnowHeight { get; set; }
    public double? MaxSnowHeight { get; set; }
  }
}