

using Microsoft.EntityFrameworkCore;

namespace AWG.Stations.handlers.Model
{
  public class StationsContext : DbContext
  {
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    => optionsBuilder.UseNpgsql("Host=docker.agricolus.com;Database=awg;Username=postgres;Password=123456/AP");

    public virtual DbSet<Stations> Stations { get; set; }
  }
}