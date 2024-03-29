﻿// <auto-generated />
using System;
using AWG.Stations.handlers.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace AWG.Stations.handlers.Migrations
{
    [DbContext(typeof(PostgresContext))]
    [Migration("20200214140157_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("AWG.Stations.handlers.Model.Station", b =>
                {
                    b.Property<long>("_internalId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<double?>("BatteryLevel")
                        .HasColumnType("double precision");

                    b.Property<string>("CategorySerialized")
                        .HasColumnName("Category")
                        .HasColumnType("text");

                    b.Property<string>("ConfigurationSerialized")
                        .HasColumnName("Configuration")
                        .HasColumnType("text");

                    b.Property<string>("ControlledAssetSerialized")
                        .HasColumnName("ControlledAsset")
                        .HasColumnType("text");

                    b.Property<string>("ControlledPropertySerialized")
                        .HasColumnName("ControlledProperty")
                        .HasColumnType("text");

                    b.Property<string>("DataProvider")
                        .HasColumnType("text");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("DateFirstUsed")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("DateInstalled")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("DateLastCalibration")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("DateLastValueReported")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("DateManufactured")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("DeviceState")
                        .HasColumnType("text");

                    b.Property<string>("FirmwareVersion")
                        .HasColumnType("text");

                    b.Property<string>("HardwareVersion")
                        .HasColumnType("text");

                    b.Property<string>("Id")
                        .HasColumnType("character varying(150)")
                        .HasMaxLength(150);

                    b.Property<string>("IpAddressSerialized")
                        .HasColumnName("IpAddress")
                        .HasColumnType("text");

                    b.Property<double>("Latitude")
                        .HasColumnType("double precision");

                    b.Property<string>("LocationSerialized")
                        .HasColumnName("Location")
                        .HasColumnType("text");

                    b.Property<double>("Longitude")
                        .HasColumnType("double precision");

                    b.Property<string>("MacAddressSerialized")
                        .HasColumnName("MacAddress")
                        .HasColumnType("text");

                    b.Property<string>("Mcc")
                        .HasColumnType("text");

                    b.Property<string>("Mnc")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("character varying(150)")
                        .HasMaxLength(150);

                    b.Property<string>("OsVersion")
                        .HasColumnType("text");

                    b.Property<string>("OwnerSerialized")
                        .HasColumnName("Owner")
                        .HasColumnType("text");

                    b.Property<string>("ProviderSerialized")
                        .HasColumnName("Provider")
                        .HasColumnType("text");

                    b.Property<string>("RefDeviceModel")
                        .HasColumnType("text");

                    b.Property<double?>("Rssi")
                        .HasColumnType("double precision");

                    b.Property<string>("SerialNumber")
                        .HasColumnType("text");

                    b.Property<string>("SoftwareVersion")
                        .HasColumnType("text");

                    b.Property<string>("Source")
                        .HasColumnType("text");

                    b.Property<string>("SupportedProtocolSerialized")
                        .HasColumnName("SupportedProtocol")
                        .HasColumnType("text");

                    b.Property<string>("Type")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("_internalId");

                    b.HasIndex("Id")
                        .HasName("station_unique");

                    b.ToTable("Stations");
                });
#pragma warning restore 612, 618
        }
    }
}
