using AutoMapper;

namespace AWG.Stations.handlers
{
  public class MappingProfileStations : Profile
  {
    public MappingProfileStations()
    {
      CreateMap<AWG.Stations.handlers.Model.Station, AWG.FIWARE.DataModels.Device>().ReverseMap();
    }
  }
}