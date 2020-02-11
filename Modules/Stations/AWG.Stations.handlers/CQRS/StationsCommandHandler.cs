using MediatR;
using AWG.Stations.handlers.Model;
using System.Threading.Tasks;
using System.Threading;
using AutoMapper;
using System.Linq;
using fiware = AWG.FIWARE.DataModels;
using AWG.Stations.core.CQRS.Command;
using Microsoft.EntityFrameworkCore;

namespace AWG.Stations.handlers.Command
{
  public class StationsCommandHandler : IRequestHandler<CreateStation, fiware.DeviceModel>,
                                        IRequestHandler<UpdateStation, fiware.DeviceModel>,
                                        IRequestHandler<DeleteStation>
  {
    private StationsContext db;
    private readonly IMediator mediator;

    public StationsCommandHandler(StationsContext db, IMediator mediator)
    {
      this.db = db;
      this.mediator = mediator;
    }

    public async Task<fiware.DeviceModel> Handle(CreateStation request, CancellationToken cancellationToken)
    {
      var station = Mapper.Map<Model.Station>(request);

      db.Stations.Add(station);

      await db.SaveChangesAsync();

      return Mapper.Map<fiware.DeviceModel>(station);
    }

    public async Task<fiware.DeviceModel> Handle(UpdateStation request, CancellationToken cancellationToken)
    {
      var station = await db.Stations.Where(f => f.Id == request.Id).FirstOrDefaultAsync();

      station = Mapper.Map<Model.Station>(request);

      await db.SaveChangesAsync();

      return Mapper.Map<fiware.DeviceModel>(station);
    }

    public async Task<Unit> Handle(DeleteStation request, CancellationToken cancellationToken)
    {
      var station = await db.Stations.Where(f => f.Id == request.Id).FirstOrDefaultAsync();

      db.Stations.Remove(station);

      await db.SaveChangesAsync();

      return Unit.Value;
    }
  }
}
