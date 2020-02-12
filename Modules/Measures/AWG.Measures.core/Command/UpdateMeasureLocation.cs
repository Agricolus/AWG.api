using MediatR;

namespace AWG.Measures.core.Command
{
  public class UpdateMeasureLocation : INotification
  {
    public string Id { get; set; }
  }
}