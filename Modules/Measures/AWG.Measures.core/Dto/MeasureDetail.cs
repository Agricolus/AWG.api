﻿using System;
using AWG.FIWARE.DataModels;
using BAMCIS.GeoJSON;

namespace AWG.Measures.core.Dto
{
  public class MeasureDetail
  {
    public string Id { get; set; }
    public string Type { get; private set; } = "WeatherObserved";
    public string DataProvider { get; set; }
    public DateTime DateModified { get; set; }
    public DateTime DateCreated { get; set; }
    public string Name { get; set; }
    public GeoJson Location { get; set; }
    public Address Address { get; set; }
    public DateTime DateObserved { get; set; }
    public string Source { get; set; }
    public string RefDevice { get; set; }
    public string RefPointOfInterest { get; set; }
    public WeatherTypeEnum? WeatherType { get; set; }
    public double? DewPoint { get; set; }
    public WeatherMeasureVisibilityEnum? Visibility { get; set; }
    public double? Temperature { get; set; }
    public double? RelativeHumidity { get; set; }
    public double? Precipitation { get; set; }
    public double? WindDirection { get; set; }
    public double? WindSpeed { get; set; }
    public double? AtmosphericPressure { get; set; }
    public PressureTendencyEnum? PressureTendency { get; set; }
    public double? SolarRadiation { get; set; }
    public double? Illuminance { get; set; }
    public double? StreamGauge { get; set; }
    public double? SnowHeight { get; set; }
  }
}