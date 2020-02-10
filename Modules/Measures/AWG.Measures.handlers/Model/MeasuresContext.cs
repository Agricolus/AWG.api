using AWG.Measures.Handlers.Model;
using Microsoft.EntityFrameworkCore;
using Toolbelt.ComponentModel.DataAnnotations;

namespace AWG.Measures.Handlers.Model
{

  public class MigrationContext : MeasuresContext
  {
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
           => optionsBuilder.UseNpgsql("Host=192.168.1.181;Database=awg;Username=postgres;Password=postgres");
  }

  public class MeasuresContext : DbContext
  {

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      // .. and invoke "BuildIndexesFromAnnotations"!
      modelBuilder.BuildIndexesFromAnnotations();
    }


    public virtual DbSet<WeatherMeasure> WeatherMeasures { get; set; }
  }
}