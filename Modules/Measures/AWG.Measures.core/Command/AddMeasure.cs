using BAMCIS.GeoJSON;
using MediatR;
using System;
using fiware = AWG.FIWARE.DataModels;

namespace AWG.Measures.core.Command
{
  public class AddMeasure : IRequest<fiware.WeatherObserved>
  {
    public fiware.WeatherObserved Model { get; set; }
  }
}