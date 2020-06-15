using System;
using BAMCIS.GeoJSON;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

/// <summary>
/// Fiware WeatherObserver data model
/// </summary>
/// <details>
/// https://github.com/FIWARE/data-models/tree/master/specs/Weather/WeatherObserved
/// </details>

namespace AWG.FIWARE.DataModels
{

  [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
  public class WeatherObservedLD : WeatherObserved
  { }

  [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
  public class WeatherObserved
  {
    public string Id { get; set; }
    public string Type { get; private set; } = "WeatherObserved";
    [JsonProperty("data_provider")]
    public string DataProvider { get; set; }
    [JsonConverter(typeof(IsoDateTimeConverter))]
    [JsonProperty("date_modified")]
    public DateTime DateModified { get; set; }
    [JsonConverter(typeof(IsoDateTimeConverter))]
    [JsonProperty("date_created")]
    public DateTime DateCreated { get; set; }
    public string Name { get; set; }
    public GeoJson Location { get; set; }
    public Address Address { get; set; }
    [JsonConverter(typeof(IsoDateTimeConverter))]
    [JsonProperty("date_observed")]
    public DateTime DateObserved { get; set; }
    public string Source { get; set; }
    [JsonProperty("ref_device")]
    public string RefDevice { get; set; }
    [JsonProperty("ref_point_of_interest")]
    public string RefPointOfInterest { get; set; }
    [JsonConverter(typeof(StringEnumConverter))]
    [JsonProperty("weather_type")]
    public WeatherTypeEnum? WeatherType { get; set; }
    [JsonProperty("dew_point")]
    public double? DewPoint { get; set; }
    [JsonConverter(typeof(StringEnumConverter))]
    public WeatherMeasureVisibilityEnum? Visibility { get; set; }
    public double? Temperature { get; set; }
    [JsonProperty("relative_humidity")]
    public double? RelativeHumidity { get; set; }
    public double? Precipitation { get; set; }
    [JsonProperty("wind_direction")]
    public double? WindDirection { get; set; }
    [JsonProperty("wind_speed")]
    public double? WindSpeed { get; set; }
    [JsonProperty("atmospheric_pressure")]
    public double? AtmosphericPressure { get; set; }
    [JsonConverter(typeof(StringEnumConverter))]
    [JsonProperty("pressure_tendency")]
    public PressureTendencyEnum? PressureTendency { get; set; }
    [JsonProperty("solar_radiation")]
    public double? SolarRadiation { get; set; }
    public double? Illuminance { get; set; }
    [JsonProperty("stream_gauge")]
    public double? StreamGauge { get; set; }
    [JsonProperty("snow_height")]
    public double? SnowHeight { get; set; }
  }
}