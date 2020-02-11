using MediatR;
using fiware = AWG.FIWARE.DataModels;

namespace AWG.Stations.core.Query
{
  public class GetStation : IRequest<fiware.Device>
  {
    public string Id { get; set; }
  }
}
