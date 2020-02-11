using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using fiware = AWG.FIWARE.DataModels;
using Toolbelt.ComponentModel.DataAnnotations.Schema;

namespace AWG.Measures.handlers.Model
{
  [Table("WeatherMeasures")]
  public class WeatherMeasure
  {

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long _internalId { get; set; }

    [Index("weather_unique", 1)]
    [StringLength(150)]
    public string Id { get; set; }
    public Uri DataProvider { get; set; }
    public DateTime DateModified { get; set; }
    public DateTime DateCreated { get; set; }

    [StringLength(150)]
    public string Name { get; set; }

    [NotMapped]
    public object Location { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }

    [NotMapped]
    public object Address { get; set; }

    [Index("weather_date", 1)]
    public DateTime DateObserved { get; set; }
    public string Source { get; set; }

    [Index("weather_date", 0)]
    [Index("weather_unique", 0)]
    public string RefDevice { get; set; }
    public string RefPointOfInterest { get; set; }
    public fiware.WeatherTypeEnum WeatherType { get; set; }
    public double DewPoint { get; set; }

    public fiware.WeatherMeasureVisibilityEnum Visibility { get; set; }
    public double Temperature { get; set; }
    public double RelativeHumidity { get; set; }
    public double Precipitation { get; set; }
    public double WindDirection { get; set; }
    public double WindSpeed { get; set; }
    public double AtmosphericPressure { get; set; }

    [NotMapped]
    public object PressureTendency { get; set; }
    public double SolarRadiation { get; set; }
    public double Illuminance { get; set; }
    public double StreamGauge { get; set; }
    public double SnowHeight { get; set; }
  }
}