using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AWG.Measures.Handlers.Migrations
{
  public partial class Initial2 : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropTable(
          name: "Stations");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.CreateTable(
          name: "Stations",
          columns: table => new
          {
            Id = table.Column<string>(type: "text", nullable: false),
            BrandName = table.Column<string>(type: "text", nullable: true),
            Category = table.Column<string>(type: "text", nullable: true),
            ControlledProperty = table.Column<string>(type: "text", nullable: true),
            DataProvider = table.Column<string>(type: "text", nullable: true),
            DateCreated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
            DateModified = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
            Description = table.Column<string>(type: "text", nullable: true),
            DeviceClass = table.Column<string>(type: "text", nullable: true),
            Documentation = table.Column<string>(type: "text", nullable: true),
            EnergyLimitationClass = table.Column<string>(type: "text", nullable: true),
            Function = table.Column<string>(type: "text", nullable: true),
            Image = table.Column<string>(type: "text", nullable: true),
            ManufacturerName = table.Column<string>(type: "text", nullable: true),
            ModelName = table.Column<string>(type: "text", nullable: true),
            Name = table.Column<string>(type: "text", nullable: true),
            Source = table.Column<string>(type: "text", nullable: true),
            SupportedProtocol = table.Column<string>(type: "text", nullable: true),
            SupportedUnits = table.Column<string>(type: "text", nullable: true)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Stations", x => x.Id);
          });
    }
  }
}
