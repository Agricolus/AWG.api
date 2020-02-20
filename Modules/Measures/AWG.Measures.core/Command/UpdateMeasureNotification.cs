using MediatR;
using dto = AWG.FIWARE.DataModels;

namespace AWG.Measures.core.Command
{
  public class UpdateMeasureNotification : INotification
  {
    public dto.WeatherObserved Measure { get; set; }

  }
}