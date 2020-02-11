using System;
using System.Collections.Generic;
using MediatR;
using fiware = AWG.FIWARE.DataModels;

namespace AWG.Stations.core.CQRS.Command
{
  public class UpdateStation : IRequest<fiware.DeviceModel>
  {
    public string Id { get; set; }
    public string Type { get; private set; } = "DeviceModel";
    public string Source { get; set; }
    public string DataProvider { get; set; }
    public string Category { get; set; }
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