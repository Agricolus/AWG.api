using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace AWG.Measures.Handlers.Migrations
{
  public partial class Initial : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.CreateTable(
          name: "Stations",
          columns: table => new
          {
            Id = table.Column<string>(nullable: false),
            Source = table.Column<string>(nullable: true),
            DataProvider = table.Column<string>(nullable: true),
            Category = table.Column<string>(nullable: true),
            DeviceClass = table.Column<string>(nullable: true),
            ControlledProperty = table.Column<string>(nullable: true),
            Function = table.Column<string>(nullable: true),
            SupportedProtocol = table.Column<string>(nullable: true),
            SupportedUnits = table.Column<string>(nullable: true),
            EnergyLimitationClass = table.Column<string>(nullable: true),
            BrandName = table.Column<string>(nullable: true),
            ModelName = table.Column<string>(nullable: true),
            ManufacturerName = table.Column<string>(nullable: true),
            Name = table.Column<string>(nullable: true),
            Description = table.Column<string>(nullable: true),
            Documentation = table.Column<string>(nullable: true),
            Image = table.Column<string>(nullable: true),
            DateModified = table.Column<DateTime>(nullable: false),
            DateCreated = table.Column<DateTime>(nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Stations", x => x.Id);
          });

      migrationBuilder.CreateTable(
          name: "WeatherMeasures",
          columns: table => new
          {
            _id = table.Column<long>(nullable: false)
                  .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            Id = table.Column<string>(maxLength: 50, nullable: true),
            DataProvider = table.Column<string>(nullable: true),
            DateModified = table.Column<DateTime>(nullable: false),
            DateCreated = table.Column<DateTime>(nullable: false),
            Name = table.Column<string>(maxLength: 50, nullable: true),
            Address = table.Column<string>(nullable: true),
            DateObserved = table.Column<DateTime>(nullable: false),
            Source = table.Column<string>(nullable: true),
            RefDevice = table.Column<string>(nullable: true),
            RefPointOfInterest = table.Column<string>(nullable: true),
            WeatherType = table.Column<int>(nullable: false),
            DewPoint = table.Column<double>(nullable: false),
            Visibility = table.Column<int>(maxLength: 20, nullable: false),
            Temperature = table.Column<double>(nullable: false),
            RelativeHumidity = table.Column<double>(nullable: false),
            Precipitation = table.Column<double>(nullable: false),
            WindDirection = table.Column<double>(nullable: false),
            WindSpeed = table.Column<double>(nullable: false),
            AtmosphericPressure = table.Column<double>(nullable: false),
            PressureTendency = table.Column<string>(maxLength: 20, nullable: true),
            SolarRadiation = table.Column<double>(nullable: false),
            Illuminance = table.Column<double>(nullable: false),
            StreamGauge = table.Column<double>(nullable: false),
            SnowHeight = table.Column<double>(nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_WeatherMeasures", x => x._id);
          });
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropTable(
          name: "Stations");

      migrationBuilder.DropTable(
          name: "WeatherMeasures");
    }
  }
}
