using Microsoft.EntityFrameworkCore;
using Toolbelt.ComponentModel.DataAnnotations;

namespace AWG.Measures.handlers.Model
{
  public class PostgresContext : MeasuresContext
  {
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
           => optionsBuilder.UseNpgsql("Host=192.168.1.181;Database=awg;Username=postgres;Password=postgres");
  }

  public class MySqlContext : MeasuresContext
  {
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseMySql("Server=192.168.1.181;Database=awg;User=root;Password=example;");
  }

  public class MeasuresContext : DbContext
  {
    public MeasuresContext() { }
    public MeasuresContext(DbContextOptions<MeasuresContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      // .. and invoke "BuildIndexesFromAnnotations"!
      modelBuilder.BuildIndexesFromAnnotations();
    }

    public virtual DbSet<WeatherObserved> WeatherObserved { get; set; }
  }
}