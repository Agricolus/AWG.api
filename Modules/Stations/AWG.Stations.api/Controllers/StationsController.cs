using System.Collections.Generic;
using System.Threading.Tasks;
using AWG.Common;
using AWG.Stations.core.Command;
using AWG.Stations.core.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using fiware = AWG.FIWARE.DataModels;

namespace AWG.Stations.api.Controllers
{
  [ApiController]
  [Route("api/stations")]
  public class StationsController : ControllerBase
  {
    private readonly ILogger<StationsController> logger;
    private readonly IMediator mediator;

    public StationsController(ILogger<StationsController> logger, IMediator mediator)
    {
      this.logger = logger;
      this.mediator = mediator;
    }

    [Route(""), HttpGet]
    public async Task<Paginated<fiware.Device>> GetAllActiveStations(int skip = 0, int take = 20)
    {
      return await mediator.Send(new ListAllActiveStations() { Skip = skip, Take = take });
    }

    [Route("{id}"), HttpGet]
    public async Task<fiware.Device> GetStation(string id)
    {
      return await mediator.Send(new GetStation() { Id = id });
    }

    [Route(""), HttpPost, HttpPut]
    public async Task<fiware.Device> CreateStation([FromBody] fiware.Device model)
    {
      return await mediator.Send(new CreateStation() { Model = model });
    }

    [Route("~/api/stations-ld"), HttpPost, HttpPut]
    public async Task<fiware.Device> CreateStation([FromBody] fiware.DeviceLD model)
    {
      return await mediator.Send(new CreateStation() { Model = model });
    }

    [Route("{id}"), HttpDelete]
    public async Task<Unit> DeleteStation(string id)
    {
      return await mediator.Send(new DeleteStation() { Id = id });
    }
  }
}