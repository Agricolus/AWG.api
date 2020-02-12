using MediatR;

namespace AWG.Stations.core.Command
{
  public class UpdateStationNotification : INotification
  {
    public string Id { get; set; }
  }
}