﻿// <auto-generated />
using System;
using IPAddressManagement.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ipaddressmanagement.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250125220507_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.1");

            modelBuilder.Entity("IPAddressManagement.Models.ChangeLog", b =>
                {
                    b.Property<int>("LogID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ChangeDescription")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("ChangeType")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("ChangedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("ChangedBy")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("DeviceID")
                        .HasColumnType("INTEGER");

                    b.HasKey("LogID");

                    b.HasIndex("DeviceID");

                    b.ToTable("ChangeLogs");
                });

            modelBuilder.Entity("IPAddressManagement.Models.Device", b =>
                {
                    b.Property<int>("DeviceID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Building")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Criticality")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("TEXT");

                    b.Property<string>("Department")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("EquipmentType")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Floor")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Hostname")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("IPAddress")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("TEXT");

                    b.Property<string>("MACAddress")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Room")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("TEXT");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.HasKey("DeviceID");

                    b.HasIndex("IPAddress")
                        .IsUnique();

                    b.ToTable("Devices");

                    b.HasData(
                        new
                        {
                            DeviceID = 1,
                            Building = "Eurostation",
                            City = "Brussels",
                            CreatedAt = new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Criticality = "High",
                            Department = "IT",
                            EquipmentType = "Dell PowerEdge R750",
                            Floor = 5,
                            Hostname = "server-01",
                            IPAddress = "192.168.1.100",
                            MACAddress = "00:1A:2B:3C:4D:5E",
                            PostalCode = "1000",
                            Room = "SR501",
                            Status = "Active",
                            Street = "Rue de la Loi",
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("IPAddressManagement.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            PasswordHash = "$2a$11$p7WzGeSVIdVRSSpwvxxw7OC3C3K9j2WYCx1DAA0JIJAwnLn4aDwSe",
                            Username = "admin"
                        });
                });

            modelBuilder.Entity("IPAddressManagement.Models.ChangeLog", b =>
                {
                    b.HasOne("IPAddressManagement.Models.Device", "Device")
                        .WithMany("ChangeLogs")
                        .HasForeignKey("DeviceID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Device");
                });

            modelBuilder.Entity("IPAddressManagement.Models.Device", b =>
                {
                    b.Navigation("ChangeLogs");
                });
#pragma warning restore 612, 618
        }
    }
}
