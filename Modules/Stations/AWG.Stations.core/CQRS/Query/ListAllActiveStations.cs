using MediatR;
using System.Collections.Generic;
using fiware = AWG.FIWARE.DataModels;

namespace AWG.Stations.core.Query
{
  public class ListAllActiveStations : IRequest<IEnumerable<fiware.Device>>
  {
  }
}

