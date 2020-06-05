using MediatR;

namespace AWG.Stations.core.Command
{
  public class UnsubscribeStation : IRequest
  {
    public string SubscriptionId { get; set; }
  }
}