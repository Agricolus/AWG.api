using MediatR;

namespace AWG.Stations.core.Command
{
  public class SubscribeToCBEntity : IRequest<string>
  {
    public string Id { get; set; }
  }
}