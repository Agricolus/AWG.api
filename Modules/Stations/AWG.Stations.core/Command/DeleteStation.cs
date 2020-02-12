using MediatR;

namespace AWG.Stations.core.Command
{
  public class DeleteStation : IRequest
  {
    public string Id { get; set; }
  }
}