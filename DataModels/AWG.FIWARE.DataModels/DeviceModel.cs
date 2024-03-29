using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

/// <summary>
/// Fiware DeviceModel data model
/// </summary>
/// <details>
/// https://github.com/FIWARE/data-models/tree/master/specs/Device/DeviceModel
/// </details>

namespace AWG.FIWARE.DataModels
{

  public class DeviceModelLD : DeviceModel
  {

  }

  [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy), ItemNullValueHandling = NullValueHandling.Ignore)]
  public class DeviceModel
  {
    public string Id { get; set; }
    public string Type { get; private set; } = "DeviceModel";
    public string Source { get; set; }
    public Uri DataProvider { get; set; }
    public IEnumerable<string> Category { get; set; }
    public string DeviceClass { get; set; }
    public IEnumerable<string> ControlledProperty { get; set; }
    public IEnumerable<string> Function { get; set; }
    public IEnumerable<string> SupportedProtocol { get; set; }
    public IEnumerable<string> SupportedUnits { get; set; }
    public string EnergyLimitationClass { get; set; }
    public string BrandName { get; set; }
    public string ModelName { get; set; }
    public string ManufacturerName { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public Uri Documentation { get; set; }
    public Uri Image { get; set; }
    public DateTime DateModified { get; set; }
    public DateTime DateCreated { get; set; }
  }
}