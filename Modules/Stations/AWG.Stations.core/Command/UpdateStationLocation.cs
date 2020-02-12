using MediatR;

namespace AWG.Stations.core.Command
{
  public class UpdateStationLocation : INotification
  {
    public string Id { get; set; }
  }
}