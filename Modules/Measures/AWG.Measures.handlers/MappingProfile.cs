using AutoMapper;

namespace AWG.Measures.handlers
{
  public class MappingProfile : Profile
  {

    public MappingProfile()
    {
      CreateMap<AWG.Measures.handlers.Model.WeatherObserved, AWG.FIWARE.DataModels.WeatherObserved>().ReverseMap();
    }
  }
}