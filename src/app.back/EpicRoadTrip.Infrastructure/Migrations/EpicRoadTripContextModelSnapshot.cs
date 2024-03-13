﻿// <auto-generated />
using System;
using EpicRoadTrip.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EpicRoadTrip.Infrastructure.Migrations
{
    [DbContext(typeof(EpicRoadTripContext))]
    partial class EpicRoadTripContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EpicRoadTrip.Domain.Cities.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("EpicRoadTrip.Domain.Institutions.Institution", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CityId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("Price")
                        .HasColumnType("float");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.ToTable("Institution", (string)null);
                });

            modelBuilder.Entity("EpicRoadTrip.Domain.Roadtrips.Roadtrip", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double>("Budget")
                        .HasColumnType("float");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Roadtrip", (string)null);
                });

            modelBuilder.Entity("EpicRoadTrip.Domain.Routes.Route", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CityId")
                        .HasColumnType("int");

                    b.Property<int>("CityOneId")
                        .HasColumnType("int");

                    b.Property<int>("CityTwoId")
                        .HasColumnType("int");

                    b.Property<double>("Distance")
                        .HasColumnType("float");

                    b.Property<TimeSpan>("Duration")
                        .HasColumnType("time");

                    b.Property<int>("RoadtripId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.HasIndex("RoadtripId");

                    b.ToTable("Routes");
                });

            modelBuilder.Entity("EpicRoadTrip.Domain.Transportations.Transportation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Company")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Cost")
                        .HasColumnType("float");

                    b.Property<double>("Score")
                        .HasColumnType("float");

                    b.Property<int>("TransportationType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Transportations");
                });

            modelBuilder.Entity("EpicRoadTrip.Domain.Users.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Gender")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("RouteTransportation", b =>
                {
                    b.Property<int>("RoutesId")
                        .HasColumnType("int");

                    b.Property<int>("TransportationsId")
                        .HasColumnType("int");

                    b.HasKey("RoutesId", "TransportationsId");

                    b.HasIndex("TransportationsId");

                    b.ToTable("RouteTransportation");
                });

            modelBuilder.Entity("EpicRoadTrip.Domain.Institutions.Institution", b =>
                {
                    b.HasOne("EpicRoadTrip.Domain.Cities.City", null)
                        .WithMany("Institution")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EpicRoadTrip.Domain.Roadtrips.Roadtrip", b =>
                {
                    b.HasOne("EpicRoadTrip.Domain.Users.User", null)
                        .WithMany("Roadtrips")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EpicRoadTrip.Domain.Routes.Route", b =>
                {
                    b.HasOne("EpicRoadTrip.Domain.Cities.City", null)
                        .WithMany("Routes")
                        .HasForeignKey("CityId");

                    b.HasOne("EpicRoadTrip.Domain.Roadtrips.Roadtrip", null)
                        .WithMany("Routes")
                        .HasForeignKey("RoadtripId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RouteTransportation", b =>
                {
                    b.HasOne("EpicRoadTrip.Domain.Routes.Route", null)
                        .WithMany()
                        .HasForeignKey("RoutesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EpicRoadTrip.Domain.Transportations.Transportation", null)
                        .WithMany()
                        .HasForeignKey("TransportationsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EpicRoadTrip.Domain.Cities.City", b =>
                {
                    b.Navigation("Institution");

                    b.Navigation("Routes");
                });

            modelBuilder.Entity("EpicRoadTrip.Domain.Roadtrips.Roadtrip", b =>
                {
                    b.Navigation("Routes");
                });

            modelBuilder.Entity("EpicRoadTrip.Domain.Users.User", b =>
                {
                    b.Navigation("Roadtrips");
                });
#pragma warning restore 612, 618
        }
    }
}
