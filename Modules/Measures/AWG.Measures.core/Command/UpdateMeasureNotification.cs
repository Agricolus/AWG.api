using MediatR;

namespace AWG.Measures.core.Command
{
  public class UpdateMeasureNotification : INotification
  {
    public string Id { get; set; }
  }
}