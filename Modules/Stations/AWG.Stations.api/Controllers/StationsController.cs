using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AWG.Stations.api.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class StationsController : ControllerBase
  {
    private readonly ILogger<StationsController> logger;
    private readonly IMediator mediator;

    public StationsController(ILogger<StationsController> logger, IMediator mediator)
    {
      this.logger = logger;
      this.mediator = mediator;
    }

    [HttpGet]
    public IActionResult Test()
    {
      return Ok();
    }
  }
}