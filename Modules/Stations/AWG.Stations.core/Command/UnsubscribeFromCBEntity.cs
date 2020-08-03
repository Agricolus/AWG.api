using MediatR;

namespace AWG.Stations.core.Command
{
  public class UnsubscribeFromCBEntity : IRequest
  {
    public string SubscriptionId { get; set; }
  }
}