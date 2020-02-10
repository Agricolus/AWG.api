using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AWG.Measures.api.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class MeasuresController : ControllerBase
  {
    private readonly ILogger<MeasuresController> logger;
    private readonly IMediator mediator;

    public MeasuresController(ILogger<MeasuresController> logger, IMediator mediator)
    {
      this.logger = logger;
      this.mediator = mediator;
    }
  }
}