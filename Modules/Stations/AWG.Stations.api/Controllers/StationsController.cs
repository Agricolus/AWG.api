using System.Collections.Generic;
using System.Threading.Tasks;
using AWG.Stations.core.CQRS.Command;
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
    public async Task<IEnumerable<fiware.DeviceModel>> GetAllActiveStations()
    {
      return await mediator.Send(new ListAllActiveStations());
    }

    [Route("{id}"), HttpGet]
    public async Task<fiware.DeviceModel> GetStation(string id)
    {
      return await mediator.Send(new GetStation() { Id = id });
    }

    [Route("station"), HttpPost]
    public async Task<fiware.DeviceModel> CreateStation(string organizationId, [FromBody] CreateStation model)
    {
      return await mediator.Send(model);
    }

    [Route("station/{id}"), HttpPut]
    public async Task<fiware.DeviceModel> UpdateStation(string id, [FromBody] UpdateStation model)
    {
      return await mediator.Send(model);
    }

    [Route("station/{id}"), HttpDelete]
    public async Task<Unit> DeleteStation(string id)
    {
      return await mediator.Send(new DeleteStation() { Id = id });
    }
  }
}