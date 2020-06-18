using MediatR;
using System;

namespace AWG.Measures.core.Command
{
  public class DeleteMeasure : IRequest
  {
    public string Id { get; set; }
  }
}