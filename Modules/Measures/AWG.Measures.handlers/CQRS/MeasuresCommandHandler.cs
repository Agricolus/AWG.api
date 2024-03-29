﻿using MediatR;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using AWG.Measures.handlers.Model;
using AWG.Measures.core.Command;
using fiware = AWG.FIWARE.DataModels;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AWG.Measures.handlers.Command
{
  public class MeasuresCommandHandler : IRequestHandler<AddMeasure, fiware.WeatherObserved>,
                                        IRequestHandler<DeleteMeasure>
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
      if (request.Model.DateObserved == null)
        return request.Model;

      var measure = mapper.Map<WeatherObserved>(request.Model);

      var oldmeasure = await db.WeatherObserved.Where(f => f.RefDevice == measure.RefDevice && f.DateObserved == measure.DateObserved).FirstOrDefaultAsync();

      if (oldmeasure != null)
        return request.Model;

      var now = DateTime.UtcNow;

      measure.Id = $"urn:ngsi-ld:WeatherObserved:{Guid.NewGuid().ToString()}";
      measure.DateCreated = now;
      measure.DateModified = now;

      db.WeatherObserved.Add(measure);

      await db.SaveChangesAsync();

      var result = mapper.Map<fiware.WeatherObserved>(measure);

      await mediator.Publish(new UpdateMeasureNotification() { Measure = result });

      return result;
    }

    public async Task<Unit> Handle(DeleteMeasure request, CancellationToken cancellationToken)
    {
      var measure = await db.WeatherObserved.Where(f => f.Id == request.Id).FirstOrDefaultAsync();

      if (measure == null)
        return Unit.Value;

      db.WeatherObserved.Remove(measure);

      await db.SaveChangesAsync();

      return Unit.Value;
    }
  }
}
