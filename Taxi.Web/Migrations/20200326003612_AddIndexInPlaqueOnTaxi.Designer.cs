﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Taxi.Web.Data;

namespace Taxi.Web.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20200326003612_AddIndexInPlaqueOnTaxi")]
    partial class AddIndexInPlaqueOnTaxi
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Taxi.Web.Data.Entities.TaxiEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Plaque")
                        .IsRequired()
                        .HasMaxLength(6);

                    b.HasKey("Id");

                    b.HasIndex("Plaque")
                        .IsUnique();

                    b.ToTable("Taxis");
                });

            modelBuilder.Entity("Taxi.Web.Data.Entities.TripDetailEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date");

                    b.Property<double>("Latitude");

                    b.Property<double>("Longitude");

                    b.Property<int?>("TripId");

                    b.HasKey("Id");

                    b.HasIndex("TripId");

                    b.ToTable("TripDetails");
                });

            modelBuilder.Entity("Taxi.Web.Data.Entities.TripEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("EndDate");

                    b.Property<float>("Qualification");

                    b.Property<string>("Remarks");

                    b.Property<string>("Source")
                        .HasMaxLength(100);

                    b.Property<double>("SourceLatitude");

                    b.Property<double>("SourceLongitude");

                    b.Property<DateTime>("StartDate");

                    b.Property<string>("Target")
                        .HasMaxLength(100);

                    b.Property<double>("TargetLatitude");

                    b.Property<double>("TargetLongitude");

                    b.Property<int?>("TaxiId");

                    b.HasKey("Id");

                    b.HasIndex("TaxiId");

                    b.ToTable("Trips");
                });

            modelBuilder.Entity("Taxi.Web.Data.Entities.TripDetailEntity", b =>
                {
                    b.HasOne("Taxi.Web.Data.Entities.TripEntity", "Trip")
                        .WithMany("TripDetails")
                        .HasForeignKey("TripId");
                });

            modelBuilder.Entity("Taxi.Web.Data.Entities.TripEntity", b =>
                {
                    b.HasOne("Taxi.Web.Data.Entities.TaxiEntity", "Taxi")
                        .WithMany("Trips")
                        .HasForeignKey("TaxiId");
                });
#pragma warning restore 612, 618
        }
    }
}
