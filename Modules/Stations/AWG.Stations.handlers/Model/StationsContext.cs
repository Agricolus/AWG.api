

using Microsoft.EntityFrameworkCore;

namespace AWG.Stations.handlers.Model
{
  public class StationsContext : DbContext
  {
    public virtual DbSet<Station> Stations { get; set; }
  }
}