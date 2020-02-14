using Microsoft.EntityFrameworkCore;
using Toolbelt.ComponentModel.DataAnnotations;

namespace AWG.Stations.handlers.Model
{
  public class PostgresContext : StationsContext
  {
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
           => optionsBuilder.UseNpgsql("Host=192.168.1.181;Database=awg;Username=postgres;Password=postgres");
  }

  public class MySqlContext : StationsContext
  {
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
           => optionsBuilder.UseMySql("Server=192.168.1.181;Database=awg;User=root;Password=example;");
  }

  public class StationsContext : DbContext
  {
    public StationsContext() { }
    public StationsContext(DbContextOptions<StationsContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      // .. and invoke "BuildIndexesFromAnnotations"!
      modelBuilder.BuildIndexesFromAnnotations();
    }

    public virtual DbSet<Station> Stations { get; set; }
  }
}