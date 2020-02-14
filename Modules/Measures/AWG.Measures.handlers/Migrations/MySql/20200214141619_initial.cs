using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AWG.Measures.handlers.Migrations.MySql
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WeatherObserved",
                columns: table => new
                {
                    _internalId = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Id = table.Column<string>(maxLength: 150, nullable: true),
                    Type = table.Column<string>(nullable: true),
                    DataProvider = table.Column<string>(nullable: true),
                    DateModified = table.Column<DateTime>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(maxLength: 150, nullable: true),
                    Location = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    Latitude = table.Column<double>(nullable: false),
                    Longitude = table.Column<double>(nullable: false),
                    DateObserved = table.Column<DateTime>(nullable: false),
                    Source = table.Column<string>(nullable: true),
                    RefDevice = table.Column<string>(nullable: true),
                    RefPointOfInterest = table.Column<string>(nullable: true),
                    WeatherType = table.Column<int>(nullable: true),
                    DewPoint = table.Column<double>(nullable: true),
                    Visibility = table.Column<int>(nullable: true),
                    Temperature = table.Column<double>(nullable: true),
                    RelativeHumidity = table.Column<double>(nullable: true),
                    Precipitation = table.Column<double>(nullable: true),
                    WindDirection = table.Column<double>(nullable: true),
                    WindSpeed = table.Column<double>(nullable: true),
                    AtmosphericPressure = table.Column<double>(nullable: true),
                    PressureTendency = table.Column<int>(nullable: true),
                    SolarRadiation = table.Column<double>(nullable: true),
                    Illuminance = table.Column<double>(nullable: true),
                    StreamGauge = table.Column<double>(nullable: true),
                    SnowHeight = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherObserved", x => x._internalId);
                });

            migrationBuilder.CreateIndex(
                name: "weather_date",
                table: "WeatherObserved",
                columns: new[] { "RefDevice", "DateObserved" });

            migrationBuilder.CreateIndex(
                name: "weather_unique",
                table: "WeatherObserved",
                columns: new[] { "RefDevice", "Id" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WeatherObserved");
        }
    }
}
