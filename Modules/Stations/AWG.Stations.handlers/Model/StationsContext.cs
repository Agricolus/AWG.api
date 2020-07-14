using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Toolbelt.ComponentModel.DataAnnotations;

namespace AWG.Stations.handlers.Model
{
  public class PostgresContext : StationsContext
  {
    private string connstring;
    public PostgresContext(IConfiguration configuration)
    {
      this.connstring = configuration.GetConnectionString("AWGPostgreContext");
      System.Console.WriteLine($"PostgresContext connection string: {this.connstring}");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
           => optionsBuilder.UseNpgsql(this.connstring);
  }

  public class MySqlContext : StationsContext
  {
    private string connstring;
    public MySqlContext(IConfiguration configuration)
    {
      this.connstring = configuration.GetConnectionString("AWGMySqlContext");
      System.Console.WriteLine($"MySqlContext connection string: {this.connstring}");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
           => optionsBuilder.UseMySql(this.connstring);
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