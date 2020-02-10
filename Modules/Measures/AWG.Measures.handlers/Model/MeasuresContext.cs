using AWG.Measures.Handlers.Model;
using Microsoft.EntityFrameworkCore;

namespace AWG.Measures.Handlers.Model
{
  public class MeasuresContext : DbContext
  {
    public virtual DbSet<WeatherMeasure> WeatherMeasures { get; set; }
  }
}