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
  public class StationsQueryHandler : IRequestHandler<ListAllActiveStations, IEnumerable<fiware.DeviceModel>>,
                                      IRequestHandler<GetStation, fiware.DeviceModel>
  {
    private StationsContext db;
    private readonly IMediator mediator;

    public StationsQueryHandler(StationsContext db, IMediator mediator)
    {
      this.db = db;
      this.mediator = mediator;
    }

    public async Task<IEnumerable<fiware.DeviceModel>> Handle(ListAllActiveStations request, CancellationToken cancellationToken)
    {
      return await db.Stations.ProjectTo<fiware.DeviceModel>().ToListAsync();
    }

    public async Task<fiware.DeviceModel> Handle(GetStation request, CancellationToken cancellationToken)
    {
      return await db.Stations.Where(f => f.Id == request.Id).ProjectTo<fiware.DeviceModel>().FirstOrDefaultAsync();
    }
  }
}