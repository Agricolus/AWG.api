using MediatR;

namespace AWG.Stations.core.Command
{
  public class CreateOrUpdateCBEntity : IRequest<string>
  {
    public string Id { get; set; }
  }
}