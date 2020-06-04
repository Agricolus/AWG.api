using MediatR;

namespace AWG.Stations.core.Command
{
  public class SubscribeStation : IRequest<string>
  {
    public string Id { get; set; }
  }
}