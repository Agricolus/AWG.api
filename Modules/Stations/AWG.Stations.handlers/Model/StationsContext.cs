

using Microsoft.EntityFrameworkCore;

namespace AWG.Stations.handlers.Model
{


  public class MigrationContext : StationsContext
  {
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
           => optionsBuilder.UseNpgsql("Host=192.168.1.181;Database=awg;Username=postgres;Password=postgres");
  }

  public class StationsContext : DbContext
  {

    public StationsContext() { }
    public StationsContext(DbContextOptions<StationsContext> options) : base(options) { }

    public virtual DbSet<Station> Stations { get; set; }
  }
}