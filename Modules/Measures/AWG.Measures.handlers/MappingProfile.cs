using AutoMapper;

namespace AWG.Measures.handlers
{
  public class MappingProfile : Profile
  {

    public MappingProfile()
    {
      CreateMap<AWG.Measures.core.Command.AddMeasure, AWG.Measures.handlers.Model.WeatherMeasure>();
      CreateMap<AWG.Measures.handlers.Model.WeatherMeasure, AWG.FIWARE.DataModels.WeatherObserved>();
    }
  }
}