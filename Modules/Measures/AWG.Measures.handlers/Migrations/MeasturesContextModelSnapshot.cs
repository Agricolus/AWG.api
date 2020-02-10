﻿// <auto-generated />
using System;
using AWG.Measures.handlers.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace AWG.Measures.handlers.Migrations
{
    [DbContext(typeof(MeasturesContext))]
    partial class MeasturesContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("AWG.Measures.handlers.Model.WeatherMeasure", b =>
                {
                    b.Property<long>("_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Address")
                        .HasColumnType("text");

                    b.Property<double>("AtmosphericPressure")
                        .HasColumnType("double precision");

                    b.Property<string>("DataProvider")
                        .HasColumnType("text");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("DateObserved")
                        .HasColumnType("timestamp without time zone");

                    b.Property<double>("DewPoint")
                        .HasColumnType("double precision");

                    b.Property<string>("Id")
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.Property<double>("Illuminance")
                        .HasColumnType("double precision");

                    b.Property<string>("Name")
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.Property<double>("Precipitation")
                        .HasColumnType("double precision");

                    b.Property<string>("PressureTendency")
                        .HasColumnType("character varying(20)")
                        .HasMaxLength(20);

                    b.Property<string>("RefDevice")
                        .HasColumnType("text");

                    b.Property<string>("RefPointOfInterest")
                        .HasColumnType("text");

                    b.Property<double>("RelativeHumidity")
                        .HasColumnType("double precision");

                    b.Property<double>("SnowHeight")
                        .HasColumnType("double precision");

                    b.Property<double>("SolarRadiation")
                        .HasColumnType("double precision");

                    b.Property<string>("Source")
                        .HasColumnType("text");

                    b.Property<double>("StreamGauge")
                        .HasColumnType("double precision");

                    b.Property<double>("Temperature")
                        .HasColumnType("double precision");

                    b.Property<int>("Visibility")
                        .HasColumnType("integer")
                        .HasMaxLength(20);

                    b.Property<int>("WeatherType")
                        .HasColumnType("integer");

                    b.Property<double>("WindDirection")
                        .HasColumnType("double precision");

                    b.Property<double>("WindSpeed")
                        .HasColumnType("double precision");

                    b.HasKey("_id");

                    b.ToTable("WeatherMeasures");
                });
#pragma warning restore 612, 618
        }
    }
}
