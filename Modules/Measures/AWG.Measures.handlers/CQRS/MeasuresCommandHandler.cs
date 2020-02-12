using MediatR;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using AWG.Measures.handlers.Model;
using AWG.Measures.core.Command;
using fiware = AWG.FIWARE.DataModels;
using AutoMapper;

namespace AWG.Measures.handlers.Command
{
  public class MeasuresCommandHandler : IRequestHandler<AddMeasure, fiware.WeatherObserved>
  {
    private readonly MeasuresContext db;
    private readonly IMediator mediator;
    private readonly IMapper mapper;

    public MeasuresCommandHandler(MeasuresContext db, IMediator mediator, IMapper mapper)
    {
      this.db = db;
      this.mediator = mediator;
      this.mapper = mapper;
    }

    public async Task<fiware.WeatherObserved> Handle(AddMeasure request, CancellationToken cancellationToken)
    {
      var measure = db.WeatherMeasures.Where(m => m.RefDevice == request.Model.RefDevice && m.Id == request.Model.Id).FirstOrDefault();

      if (measure == null)
      {
        measure = mapper.Map<WeatherMeasure>(request.Model);
        measure.DateCreated = DateTime.UtcNow;
        db.WeatherMeasures.Add(measure);
      }
      else
      {
        mapper.Map(request, measure);
      }

      measure.DateModified = DateTime.UtcNow;

      await db.SaveChangesAsync();

      return mapper.Map<fiware.WeatherObserved>(measure);
    }
  }
}
