using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AWG.Stations.handlers.Migrations.MySql
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Stations",
                columns: table => new
                {
                    _internalId = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Id = table.Column<string>(maxLength: 150, nullable: true),
                    Type = table.Column<string>(nullable: true),
                    Source = table.Column<string>(nullable: true),
                    DataProvider = table.Column<string>(nullable: true),
                    Category = table.Column<string>(nullable: true),
                    ControlledProperty = table.Column<string>(nullable: true),
                    ControlledAsset = table.Column<string>(nullable: true),
                    Mnc = table.Column<string>(nullable: true),
                    Mcc = table.Column<string>(nullable: true),
                    MacAddress = table.Column<string>(nullable: true),
                    IpAddress = table.Column<string>(nullable: true),
                    SupportedProtocol = table.Column<string>(nullable: true),
                    Configuration = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 150, nullable: true),
                    Location = table.Column<string>(nullable: true),
                    Latitude = table.Column<double>(nullable: false),
                    Longitude = table.Column<double>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    DateInstalled = table.Column<DateTime>(nullable: true),
                    DateFirstUsed = table.Column<DateTime>(nullable: true),
                    DateManufactured = table.Column<DateTime>(nullable: true),
                    HardwareVersion = table.Column<string>(nullable: true),
                    SoftwareVersion = table.Column<string>(nullable: true),
                    FirmwareVersion = table.Column<string>(nullable: true),
                    OsVersion = table.Column<string>(nullable: true),
                    DateLastCalibration = table.Column<DateTime>(nullable: true),
                    SerialNumber = table.Column<string>(nullable: true),
                    Provider = table.Column<string>(nullable: true),
                    RefDeviceModel = table.Column<string>(nullable: true),
                    BatteryLevel = table.Column<double>(nullable: true),
                    Rssi = table.Column<double>(nullable: true),
                    DeviceState = table.Column<string>(nullable: true),
                    DateLastValueReported = table.Column<DateTime>(nullable: true),
                    Value = table.Column<string>(nullable: true),
                    DateModified = table.Column<DateTime>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    Owner = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stations", x => x._internalId);
                });

            migrationBuilder.CreateIndex(
                name: "station_unique",
                table: "Stations",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Stations");
        }
    }
}
