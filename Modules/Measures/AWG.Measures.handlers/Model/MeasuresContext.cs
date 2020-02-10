using AWG.Measures.Handlers.Model;
using Microsoft.EntityFrameworkCore;

namespace AWG.Measures.handlers.Model
{
  public class MeasturesContext : DbContext
  {
    public virtual DbSet<WeatherMeasure> WeatherMeasures { get; set; }
  }
}