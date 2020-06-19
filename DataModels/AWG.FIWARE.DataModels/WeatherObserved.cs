using System;
using AWG.FIWARE.Serializers;
using AWG.FIWARE.Serializers.Attributes;
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
  public class WeatherObservedLD : WeatherObserved
  { }

  [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy), ItemNullValueHandling = NullValueHandling.Ignore)]
  public class WeatherObserved : IContextBrokerEntity
  {
    public string Id { get; set; }

    public string Type { get; private set; } = "WeatherObserved";

    public string DataProvider { get; set; }

    [JsonConverter(typeof(IsoDateTimeConverter))]
    public DateTime? DateModified { get; set; }

    [JsonConverter(typeof(IsoDateTimeConverter))]
    public DateTime? DateCreated { get; set; }

    public string Name { get; set; }

    [GeoJSON]
    public GeoJson Location { get; set; }

    [PostalAddress]
    public Address Address { get; set; }

    [JsonConverter(typeof(IsoDateTimeConverter))]
    public DateTime DateObserved { get; set; }

    public string Source { get; set; }

    [Relationship]
    public string RefDevice { get; set; }

    public string RefPointOfInterest { get; set; }

    [JsonConverter(typeof(StringEnumConverter))]
    public WeatherTypeEnum? WeatherType { get; set; }

    [JsonConverter(typeof(DecimalJsonConverter))]
    public double? DewPoint { get; set; }

    [JsonConverter(typeof(StringEnumConverter))]
    public WeatherMeasureVisibilityEnum? Visibility { get; set; }

    [JsonConverter(typeof(DecimalJsonConverter))]
    public double? Temperature { get; set; }

    [JsonConverter(typeof(DecimalJsonConverter))]
    public double? RelativeHumidity { get; set; }

    [JsonConverter(typeof(DecimalJsonConverter))]
    public double? Precipitation { get; set; }

    [JsonConverter(typeof(DecimalJsonConverter))]
    public double? WindDirection { get; set; }

    [JsonConverter(typeof(DecimalJsonConverter))]
    public double? WindSpeed { get; set; }

    [JsonConverter(typeof(DecimalJsonConverter))]
    public double? AtmosphericPressure { get; set; }

    [JsonConverter(typeof(StringEnumConverter))]
    public PressureTendencyEnum? PressureTendency { get; set; }

    [JsonConverter(typeof(DecimalJsonConverter))]
    public double? SolarRadiation { get; set; }

    [JsonConverter(typeof(DecimalJsonConverter))]
    public double? Illuminance { get; set; }

    [JsonConverter(typeof(DecimalJsonConverter))]
    public double? StreamGauge { get; set; }

    [JsonConverter(typeof(DecimalJsonConverter))]
    public double? SnowHeight { get; set; }

    //previously missing
    public string StationCode { get; set; }

    public double? UvIndexMax { get; set; }
  }
}