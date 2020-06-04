using MediatR;

namespace AWG.Stations.core.Command
{
  public class UnSubscribeStation : IRequest
  {
    public string subId { get; set; }
  }
}