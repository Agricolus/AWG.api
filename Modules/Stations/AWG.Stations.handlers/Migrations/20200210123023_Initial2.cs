using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace AWG.Stations.handlers.Migrations
{
  public partial class Initial2 : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropPrimaryKey(
          name: "PK_Stations",
          table: "Stations");

      migrationBuilder.AlterColumn<string>(
          name: "Id",
          table: "Stations",
          nullable: true,
          oldClrType: typeof(string),
          oldType: "text");

      migrationBuilder.AddColumn<long>(
          name: "_Id",
          table: "Stations",
          nullable: false)
          .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

      migrationBuilder.AddPrimaryKey(
          name: "PK_Stations",
          table: "Stations",
          column: "_Id");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropPrimaryKey(
          name: "PK_Stations",
          table: "Stations");

      migrationBuilder.DropColumn(
          name: "_Id",
          table: "Stations");

      migrationBuilder.AlterColumn<string>(
          name: "Id",
          table: "Stations",
          type: "text",
          nullable: false,
          oldClrType: typeof(string),
          oldNullable: true);

      migrationBuilder.AddPrimaryKey(
          name: "PK_Stations",
          table: "Stations",
          column: "Id");
    }
  }
}
