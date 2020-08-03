using System;
using System.Collections.Generic;
using BAMCIS.GeoJSON;
using MediatR;
using fiware = AWG.FIWARE.DataModels;

namespace AWG.Stations.core.Command
{
  public class CreateStation : IRequest<fiware.Device>
  {
    public fiware.Device Model { get; set; }
    public bool CBEnabled { get; set; } = true;
  }
}