using MediatR;

namespace AWG.Stations.core.Command
{
  public class DeleteStation : IRequest
  {
    public string Id { get; set; }
    public bool CBEnabled { get; set; } = true;
  }
}