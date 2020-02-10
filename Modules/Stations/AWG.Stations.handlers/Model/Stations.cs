using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AWG.Stations.handlers.Model
{
  [Table("Stations")]
  public class Station
  {
    [Key]
    public long _Id { get; set; }
    public string Id { get; set; }
    public string Source { get; set; }
    public string DataProvider { get; set; }
    public string Category { get; set; }
    public string DeviceClass { get; set; }
    public string ControlledProperty { get; set; }
    public string Function { get; set; }
    public string SupportedProtocol { get; set; }
    public string SupportedUnits { get; set; }
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