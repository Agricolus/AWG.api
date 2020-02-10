using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AWG.FIWARE.DataModels;

namespace AWG.Measures.handlers.Model
{
  [Table("WeatherMeasures")]
  public class WeatherMeasure
  {

    [Key]
    public long _id { get; set; }

    [StringLength(50)]
    public string Id { get; set; }
    public Uri DataProvider { get; set; }
    public DateTime DateModified { get; set; }
    public DateTime DateCreated { get; set; }

    [StringLength(50)]
    public string Name { get; set; }
    // public object Location { get; set; }
    public string Address { get; set; }
    public DateTime DateObserved { get; set; }
    public string Source { get; set; }
    public string RefDevice { get; set; }
    public string RefPointOfInterest { get; set; }
    public WeatherTypeEnum WeatherType { get; set; }
    public double DewPoint { get; set; }
    [StringLength(20)]
    public WeatherMeasureVisibilityEnum Visibility { get; set; }
    public double Temperature { get; set; }
    public double RelativeHumidity { get; set; }
    public double Precipitation { get; set; }
    public double WindDirection { get; set; }
    public double WindSpeed { get; set; }
    public double AtmosphericPressure { get; set; }
    [StringLength(20)]
    public string PressureTendency { get; set; }
    public double SolarRadiation { get; set; }
    public double Illuminance { get; set; }
    public double StreamGauge { get; set; }
    public double SnowHeight { get; set; }
  }
}