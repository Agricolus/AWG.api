using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace AWG.Measures.handlers.Migrations
{
  public partial class DataModelFixId : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropPrimaryKey(
          name: "PK_WeatherMeasures",
          table: "WeatherMeasures");

      migrationBuilder.DropColumn(
          name: "_id",
          table: "WeatherMeasures");

      migrationBuilder.AddColumn<long>(
          name: "_internalId",
          table: "WeatherMeasures",
          nullable: false)
          .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

      migrationBuilder.AddPrimaryKey(
          name: "PK_WeatherMeasures",
          table: "WeatherMeasures",
          column: "_internalId");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropPrimaryKey(
          name: "PK_WeatherMeasures",
          table: "WeatherMeasures");

      migrationBuilder.DropColumn(
          name: "_internalId",
          table: "WeatherMeasures");

      migrationBuilder.AddColumn<long>(
          name: "_id",
          table: "WeatherMeasures",
          type: "bigint",
          nullable: false)
          .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

      migrationBuilder.AddPrimaryKey(
          name: "PK_WeatherMeasures",
          table: "WeatherMeasures",
          column: "_id");
    }
  }
}
