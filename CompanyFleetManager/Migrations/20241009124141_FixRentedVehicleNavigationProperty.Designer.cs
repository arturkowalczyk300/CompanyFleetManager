﻿// <auto-generated />
using System;
using CompanyFleetManager;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CompanyFleetManager.Migrations
{
    [DbContext(typeof(FleetDatabaseContext))]
    [Migration("20241009124141_FixRentedVehicleNavigationProperty")]
    partial class FixRentedVehicleNavigationProperty
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CompanyFleetManager.Models.Entities.Employee", b =>
                {
                    b.Property<int>("EmployeeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmployeeId"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DrivingLicenseCategories")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateOnly>("DrivingLicenseValidity")
                        .HasColumnType("date");

                    b.Property<string>("Forename")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateOnly>("HiredUntil")
                        .HasColumnType("date");

                    b.Property<string>("Middlename")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("NationalIdentityNumber")
                        .HasColumnType("bigint");

                    b.Property<string>("Occupation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PrivatePhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WorkPhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EmployeeId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("CompanyFleetManager.Models.Entities.Rental", b =>
                {
                    b.Property<int>("RentalId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RentalId"));

                    b.Property<DateTime?>("FactualReturningDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("PlannedReturningDate")
                        .HasColumnType("datetime2");

                    b.Property<DateOnly>("RentalDate")
                        .HasColumnType("date");

                    b.Property<int>("RentedVehicleId")
                        .HasColumnType("int");

                    b.Property<int>("RentingEmployeeId")
                        .HasColumnType("int");

                    b.HasKey("RentalId");

                    b.HasIndex("RentedVehicleId");

                    b.HasIndex("RentingEmployeeId");

                    b.ToTable("Rentals");
                });

            modelBuilder.Entity("CompanyFleetManager.Models.Entities.Vehicle", b =>
                {
                    b.Property<int>("VehicleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VehicleId"));

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDamaged")
                        .HasColumnType("bit");

                    b.Property<string>("LicencePlateNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Mileage")
                        .HasColumnType("int");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProductionYear")
                        .HasColumnType("int");

                    b.Property<DateOnly>("VehicleInspectionValidity")
                        .HasColumnType("date");

                    b.HasKey("VehicleId");

                    b.ToTable("Vehicles");
                });

            modelBuilder.Entity("CompanyFleetManager.Models.Entities.Rental", b =>
                {
                    b.HasOne("CompanyFleetManager.Models.Entities.Vehicle", "RentedVehicle")
                        .WithMany("Rentals")
                        .HasForeignKey("RentedVehicleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CompanyFleetManager.Models.Entities.Employee", "RentingEmployee")
                        .WithMany("Rentals")
                        .HasForeignKey("RentingEmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RentedVehicle");

                    b.Navigation("RentingEmployee");
                });

            modelBuilder.Entity("CompanyFleetManager.Models.Entities.Employee", b =>
                {
                    b.Navigation("Rentals");
                });

            modelBuilder.Entity("CompanyFleetManager.Models.Entities.Vehicle", b =>
                {
                    b.Navigation("Rentals");
                });
#pragma warning restore 612, 618
        }
    }
}
