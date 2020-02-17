using AutoMapper;

namespace AWG.Stations.handlers
{
  public class MappingProfile : Profile
  {

    public MappingProfile()
    {
      CreateMap<AWG.Stations.handlers.Model.Station, AWG.FIWARE.DataModels.Device>().ReverseMap();
    }
  }
}