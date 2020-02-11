using MediatR;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using AWG.Measures.handlers.Model;
using AWG.Measures.core.Command;
using fiware = AWG.FIWARE.DataModels;
using AutoMapper;

namespace AWG.Measures.handlers.Command
{
  public class MeasuresCommandHandler : IRequestHandler<AddMeasure, fiware.WeatherObserved>
  {
    private MeasuresContext db;
    private readonly IMediator mediator;

    public MeasuresCommandHandler(MeasuresContext db, IMediator mediator)
    {
      this.db = db;
      this.mediator = mediator;
    }

    public async Task<fiware.WeatherObserved> Handle(AddMeasure request, CancellationToken cancellationToken)
    {
      var measure = db.WeatherMeasures.Where(m => m.RefDevice == request.RefDevice && m.DateObserved == request.DateObserved).FirstOrDefault();

      if (measure == null)
      {
        measure = Mapper.Map<WeatherMeasure>(request);
        measure.DateCreated = DateTime.UtcNow;
        db.WeatherMeasures.Add(measure);
      }
      else
      {
        Mapper.Map(request, measure);
      }

      measure.DateModified = DateTime.UtcNow;

      await db.SaveChangesAsync();
      return Mapper.Map<fiware.WeatherObserved>(measure);
    }
  }
}
