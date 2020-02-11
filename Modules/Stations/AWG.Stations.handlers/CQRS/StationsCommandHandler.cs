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
  public class StationsCommandHandler : IRequestHandler<CreateStation, fiware.Device>,
                                        IRequestHandler<UpdateStation, fiware.Device>,
                                        IRequestHandler<DeleteStation>
  {
    private readonly StationsContext db;
    private readonly IMediator mediator;
    private readonly IMapper mapper;

    public StationsCommandHandler(StationsContext db, IMediator mediator, IMapper mapper)
    {
      this.db = db;
      this.mediator = mediator;
      this.mapper = mapper;
    }

    public async Task<fiware.Device> Handle(CreateStation request, CancellationToken cancellationToken)
    {
      var station = mapper.Map<Model.Station>(request);

      db.Stations.Add(station);

      await db.SaveChangesAsync();

      return mapper.Map<fiware.Device>(station);
    }

    public async Task<fiware.Device> Handle(UpdateStation request, CancellationToken cancellationToken)
    {
      var station = await db.Stations.Where(f => f.Id == request.Id).FirstOrDefaultAsync();

      station = mapper.Map<Model.Station>(request);

      await db.SaveChangesAsync();

      return mapper.Map<fiware.Device>(station);
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
