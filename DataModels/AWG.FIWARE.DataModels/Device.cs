using System;
using System.Collections.Generic;
using BAMCIS.GeoJSON;

/// <summary>
/// Fiware Device data model
/// </summary>
/// <details>
/// https://github.com/FIWARE/data-models/tree/master/specs/Device/Device
/// </details>

namespace AWG.FIWARE.DataModels
{
  public class DeviceLD : Device
  {

  }

  public class Device
  {
    public string Id { get; set; }
    public string Type { get; private set; } = "Device";
    public string Source { get; set; }
    public string DataProvider { get; set; }
    public IEnumerable<string> Category { get; set; }
    public IEnumerable<string> ControlledProperty { get; set; }
    public IEnumerable<string> ControlledAsset { get; set; }
    public string Mnc { get; set; }
    public string Mcc { get; set; }
    public IEnumerable<string> MacAddress { get; set; }
    public IEnumerable<string> IpAddress { get; set; }
    public IEnumerable<string> SupportedProtocol { get; set; }
    public object Configuration { get; set; }
    public GeoJson Location { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime? DateInstalled { get; set; }
    public DateTime? DateFirstUsed { get; set; }
    public DateTime? DateManufactured { get; set; }
    public string HardwareVersion { get; set; }
    public string SoftwareVersion { get; set; }
    public string FirmwareVersion { get; set; }
    public string OsVersion { get; set; }
    public DateTime? DateLastCalibration { get; set; }
    public string SerialNumber { get; set; }
    public object Provider { get; set; }
    public string RefDeviceModel { get; set; }
    public double? BatteryLevel { get; set; }
    public double? Rssi { get; set; }
    public string DeviceState { get; set; }
    public DateTime? DateLastValueReported { get; set; }
    public string Value { get; set; }
    public DateTime DateModified { get; set; }
    public DateTime DateCreated { get; set; }
    public IEnumerable<object> Owner { get; set; }
  }
}