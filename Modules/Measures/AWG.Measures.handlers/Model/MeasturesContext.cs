using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace AWG.Measures.handlers.Model
{
  public class MeasturesContext : DbContext
  {
    public virtual DbSet<WeatherMeasure> WeatherMeasures { get; set; }
  }
}