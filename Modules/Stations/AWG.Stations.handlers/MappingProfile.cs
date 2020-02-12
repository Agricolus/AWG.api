using AutoMapper;

namespace AWG.Stations.handlers
{
  public class MappingProfile : Profile
  {

    public MappingProfile()
    {
      CreateMap<AWG.Stations.core.Command.CreateStation, AWG.Stations.handlers.Model.Station>();
      CreateMap<AWG.Stations.core.Command.UpdateStation, AWG.Stations.handlers.Model.Station>();
      CreateMap<AWG.Stations.handlers.Model.Station, AWG.FIWARE.DataModels.Device>();
    }
  }
}