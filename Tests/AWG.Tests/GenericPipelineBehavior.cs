using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using AWG.Stations.handlers.Model;
using MediatR;
using AWG.Measures.handlers.Model;

namespace AWG.Tests
{
  public class GenericPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
  {
    //private readonly StationsContext db;
    private readonly MeasuresContext db;
    private readonly IMediator mediator;
    private readonly IMapper mapper;
    private readonly IConfiguration configuration;

    public GenericPipelineBehavior(/*StationsContext db,*/ MeasuresContext db, IMediator mediator, IMapper mapper, IConfiguration configuration)
    {
      this.db = db;
      this.mediator = mediator;
      this.mapper = mapper;
      this.configuration = configuration;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
      return await next();
    }
  }
}
