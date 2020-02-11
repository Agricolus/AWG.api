using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using AWG.Stations.core.Query;
using AWG.Stations.handlers.Model;
using fiware = AWG.FIWARE.DataModels;

namespace AWG.Stations.handlers.Query
{
  public class StationsQueryHandler : IRequestHandler<ListAllActiveStations, IEnumerable<fiware.Device>>,
                                      IRequestHandler<GetStation, fiware.Device>
  {
    private StationsContext db;
    private readonly IMediator mediator;

    public StationsQueryHandler(StationsContext db, IMediator mediator)
    {
      this.db = db;
      this.mediator = mediator;
    }

    public async Task<IEnumerable<fiware.Device>> Handle(ListAllActiveStations request, CancellationToken cancellationToken)
    {
      return await db.Stations.ProjectTo<fiware.Device>().ToListAsync();
    }

    public async Task<fiware.Device> Handle(GetStation request, CancellationToken cancellationToken)
    {
      return await db.Stations.Where(f => f.Id == request.Id).ProjectTo<fiware.Device>().FirstOrDefaultAsync();
    }
  }
}