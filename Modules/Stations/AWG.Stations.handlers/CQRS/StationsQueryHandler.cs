using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using AWG.Stations.core.Query;
using AWG.Stations.handlers.Model;
using fiware = AWG.FIWARE.DataModels;
using AWG.Common;
using AWG.FIWARE.DataModels;

namespace AWG.Stations.handlers.Query
{
  public class StationsQueryHandler : IRequestHandler<ListAllActiveStations, Paginated<fiware.Device>>,
                                      IRequestHandler<GetStation, fiware.Device>
  {
    private readonly StationsContext db;
    private readonly IMediator mediator;
    private readonly IMapper mapper;

    public StationsQueryHandler(StationsContext db, IMediator mediator, IMapper mapper)
    {
      this.db = db;
      this.mediator = mediator;
      this.mapper = mapper;
    }

    public async Task<fiware.Device> Handle(GetStation request, CancellationToken cancellationToken)
    {
      return await db.Stations.Where(f => f.Id == request.Id).ProjectTo<fiware.Device>(mapper.ConfigurationProvider).FirstOrDefaultAsync();
    }

    public async Task<Paginated<Device>> Handle(ListAllActiveStations request, CancellationToken cancellationToken)
    {
      var query = db.Stations.ProjectTo<fiware.Device>(mapper.ConfigurationProvider);

      return new Paginated<fiware.Device>()
      {
        Skip = request.Skip,
        Take = request.Take,
        TotalCount = await query.CountAsync(),
        Items = await query.Skip(request.Skip).Take(request.Take).ToListAsync()
      };
    }
  }
}