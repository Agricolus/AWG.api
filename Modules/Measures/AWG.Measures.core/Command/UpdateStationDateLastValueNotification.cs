using System;
using MediatR;

namespace AWG.Measures.core.Command
{
  public class UpdateStationDateLastValueNotification : INotification
  {
    public string Id { get; set; }
    public DateTime DateLastValueReported { get; set; }
  }
}