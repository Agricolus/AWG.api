using Microsoft.EntityFrameworkCore.Migrations;

namespace AWG.Measures.handlers.Migrations
{
    public partial class DataModelFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "WeatherMeasures");

            migrationBuilder.DropColumn(
                name: "PressureTendency",
                table: "WeatherMeasures");

            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "WeatherMeasures",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "WeatherMeasures",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "WeatherMeasures");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "WeatherMeasures");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "WeatherMeasures",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PressureTendency",
                table: "WeatherMeasures",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
