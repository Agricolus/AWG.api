using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Toolbelt.ComponentModel.DataAnnotations;

namespace AWG.Measures.handlers.Model
{
  public class PostgresContext : MeasuresContext
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

  public class MySqlContext : MeasuresContext
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