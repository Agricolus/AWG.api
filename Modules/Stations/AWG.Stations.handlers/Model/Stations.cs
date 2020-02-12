using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Toolbelt.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace AWG.Stations.handlers.Model
{
  [Table("Stations")]
  public class Station
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long _internalId { get; set; }

    [Index("station_unique", 0)]
    [StringLength(150)]
    public string Id { get; set; }
    public string Type { get; private set; } = "Device";
    public string Source { get; set; }
    public Uri DataProvider { get; set; }

    [NotMapped]
    public IEnumerable<string> Category { get; set; }
    [Column("Category")]
    public string CategorySerialized
    {
      get
      {
        return JsonConvert.SerializeObject(Category);
      }
      set
      {
        if (value != null)
          Category = JsonConvert.DeserializeObject<IEnumerable<string>>(value);
        else
          Category = null;
      }
    }

    [NotMapped]
    public IEnumerable<string> ControlledProperty { get; set; }
    [Column("ControlledProperty")]
    public string ControlledPropertySerialized
    {
      get
      {
        return JsonConvert.SerializeObject(ControlledProperty);
      }
      set
      {
        if (value != null)
          ControlledProperty = JsonConvert.DeserializeObject<IEnumerable<string>>(value);
        else
          ControlledProperty = null;
      }
    }

    [NotMapped]
    public IEnumerable<string> ControlledAsset { get; set; }
    [Column("ControlledAsset")]
    public string ControlledAssetSerialized
    {
      get
      {
        return JsonConvert.SerializeObject(ControlledAsset);
      }
      set
      {
        if (value != null)
          ControlledAsset = JsonConvert.DeserializeObject<IEnumerable<string>>(value);
        else
          ControlledAsset = null;
      }
    }

    public string Mnc { get; set; }
    public string Mcc { get; set; }

    [NotMapped]
    public IEnumerable<string> MacAddress { get; set; }
    [Column("MacAddress")]
    public string MacAddressSerialized
    {
      get
      {
        return JsonConvert.SerializeObject(MacAddress);
      }
      set
      {
        if (value != null)
          MacAddress = JsonConvert.DeserializeObject<IEnumerable<string>>(value);
        else
          MacAddress = null;
      }
    }

    [NotMapped]
    public IEnumerable<string> IpAddress { get; set; }
    [Column("IpAddress")]
    public string IpAddressSerialized
    {
      get
      {
        return JsonConvert.SerializeObject(IpAddress);
      }
      set
      {
        if (value != null)
          IpAddress = JsonConvert.DeserializeObject<IEnumerable<string>>(value);
        else
          IpAddress = null;
      }
    }

    [NotMapped]
    public IEnumerable<string> SupportedProtocol { get; set; }
    [Column("SupportedProtocol")]
    public string SupportedProtocolSerialized
    {
      get
      {
        return JsonConvert.SerializeObject(SupportedProtocol);
      }
      set
      {
        if (value != null)
          SupportedProtocol = JsonConvert.DeserializeObject<IEnumerable<string>>(value);
        else
          SupportedProtocol = null;
      }
    }

    [NotMapped]
    public object Configuration { get; set; }
    [Column("Configuration")]
    public string ConfigurationSerialized
    {
      get
      {
        return JsonConvert.SerializeObject(Configuration);
      }
      set
      {
        if (value != null)
          Configuration = JsonConvert.DeserializeObject<object>(value);
        else
          Configuration = null;
      }
    }

    [StringLength(150)]
    public string Name { get; set; }

    [NotMapped]
    public object Location { get; set; }
    [Column("Location")]
    public string LocationSerialized
    {
      get
      {
        return JsonConvert.SerializeObject(Location);
      }
      set
      {
        if (value != null)
          Location = JsonConvert.DeserializeObject<object>(value);
        else
          Location = null;
      }
    }

    public double Latitude { get; set; }
    public double Longitude { get; set; }
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

    [NotMapped]
    public object Provider { get; set; }
    [Column("Provider")]
    public string ProviderSerialized
    {
      get
      {
        return JsonConvert.SerializeObject(Provider);
      }
      set
      {
        if (value != null)
          Provider = JsonConvert.DeserializeObject<object>(value);
        else
          Provider = null;
      }
    }

    public string RefDeviceModel { get; set; }
    public double? BatteryLevel { get; set; }
    public double? Rssi { get; set; }
    public string DeviceState { get; set; }
    public DateTime? DateLastValueReported { get; set; }
    public string Value { get; set; }
    public DateTime DateModified { get; set; }
    public DateTime DateCreated { get; set; }

    [NotMapped]
    public IEnumerable<object> Owner { get; set; }
    [Column("Owner")]
    public string OwnerSerialized
    {
      get
      {
        return JsonConvert.SerializeObject(Owner);
      }
      set
      {
        if (value != null)
          Owner = JsonConvert.DeserializeObject<IEnumerable<object>>(value);
        else
          Owner = null;
      }
    }
  }
}