using AWG.Common;
using MediatR;
using System.Collections.Generic;
using fiware = AWG.FIWARE.DataModels;

namespace AWG.Stations.core.Query
{
  public class GetNearestStations : IRequest<Paginated<fiware.Device>>
  {
    public double Longitude { get; set; }
    public double Latitude { get; set; }
    public int Skip { get; set; }
    public int Take { get; set; }
  }
}

