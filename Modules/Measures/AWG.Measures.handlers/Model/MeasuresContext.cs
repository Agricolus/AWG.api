using Microsoft.EntityFrameworkCore;

namespace AWG.Measures.Handlers.Model
{
  public class MeasuresContext : DbContext
  {
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Host=docker.agricolus.com;Database=awg;Username=postgres;Password=123456/AP");

    public virtual DbSet<WeatherMeasure> WeatherMeasures { get; set; }
  }
}