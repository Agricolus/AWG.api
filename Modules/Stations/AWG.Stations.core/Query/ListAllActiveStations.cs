using AWG.Common;
using MediatR;
using System.Collections.Generic;
using fiware = AWG.FIWARE.DataModels;

namespace AWG.Stations.core.Query
{
  public class ListAllActiveStations : IRequest<Paginated<fiware.Device>>
  {
    public int Skip { get; set; }
    public int Take { get; set; }
  }
}

