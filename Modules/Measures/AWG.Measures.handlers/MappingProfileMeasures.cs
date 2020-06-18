using AutoMapper;

namespace AWG.Measures.handlers
{
  public class MappingProfileMeasures : Profile
  {
    public MappingProfileMeasures()
    {
      CreateMap<AWG.Measures.handlers.Model.WeatherObserved, AWG.FIWARE.DataModels.WeatherObserved>().ReverseMap();
      CreateMap<AWG.Measures.core.Dto.MeasureDetail, AWG.Measures.handlers.Model.WeatherObserved>().ReverseMap();
    }
  }
}