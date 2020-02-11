using MediatR;

namespace AWG.Stations.core.CQRS.Command
{
  public class DeleteStation : IRequest
  {
    public string Id { get; set; }
  }
}