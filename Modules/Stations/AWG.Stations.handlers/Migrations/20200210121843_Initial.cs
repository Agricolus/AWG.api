using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AWG.Stations.handlers.Migrations
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Stations");
        }
    }
}
