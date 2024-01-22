﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using server.Context;

#nullable disable

namespace server.Migrations
{
    [DbContext(typeof(OfficeDbContext))]
    partial class OfficeDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("server.Models.Domain.Booking", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("BookingDateTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<int>("SeatId")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("SeatId");

                    b.ToTable("Bookings");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BookingDateTime = new DateTime(2023, 12, 7, 14, 44, 11, 768, DateTimeKind.Local).AddTicks(9580),
                            SeatId = 1,
                            UserId = "860849a4-f4b8-4566-8ed1-918cf3d41a92",
                            UserName = "SampleUser1"
                        },
                        new
                        {
                            Id = 2,
                            BookingDateTime = new DateTime(2023, 12, 5, 14, 44, 11, 768, DateTimeKind.Local).AddTicks(9580),
                            SeatId = 2,
                            UserId = "639d660b-4724-407b-b05c-12b5f619f833",
                            UserName = "SampleUser2"
                        });
                });

            modelBuilder.Entity("server.Models.Domain.Room", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Rooms");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Store Rommet"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Lille Rommet"
                        });
                });

            modelBuilder.Entity("server.Models.Domain.Seat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("bit");

                    b.Property<int>("RoomId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoomId");

                    b.ToTable("Seats");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            IsAvailable = true,
                            RoomId = 1
                        },
                        new
                        {
                            Id = 2,
                            IsAvailable = true,
                            RoomId = 1
                        },
                        new
                        {
                            Id = 3,
                            IsAvailable = true,
                            RoomId = 1
                        },
                        new
                        {
                            Id = 4,
                            IsAvailable = true,
                            RoomId = 1
                        },
                        new
                        {
                            Id = 5,
                            IsAvailable = true,
                            RoomId = 1
                        },
                        new
                        {
                            Id = 6,
                            IsAvailable = true,
                            RoomId = 1
                        },
                        new
                        {
                            Id = 7,
                            IsAvailable = true,
                            RoomId = 1
                        },
                        new
                        {
                            Id = 8,
                            IsAvailable = true,
                            RoomId = 1
                        },
                        new
                        {
                            Id = 9,
                            IsAvailable = true,
                            RoomId = 1
                        },
                        new
                        {
                            Id = 10,
                            IsAvailable = true,
                            RoomId = 1
                        },
                        new
                        {
                            Id = 11,
                            IsAvailable = true,
                            RoomId = 2
                        },
                        new
                        {
                            Id = 12,
                            IsAvailable = true,
                            RoomId = 2
                        },
                        new
                        {
                            Id = 13,
                            IsAvailable = true,
                            RoomId = 2
                        },
                        new
                        {
                            Id = 14,
                            IsAvailable = true,
                            RoomId = 2
                        },
                        new
                        {
                            Id = 15,
                            IsAvailable = true,
                            RoomId = 2
                        });
                });

            modelBuilder.Entity("server.Models.Domain.Booking", b =>
                {
                    b.HasOne("server.Models.Domain.Seat", "Seat")
                        .WithMany("Bookings")
                        .HasForeignKey("SeatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Seat");
                });

            modelBuilder.Entity("server.Models.Domain.Seat", b =>
                {
                    b.HasOne("server.Models.Domain.Room", null)
                        .WithMany("Seats")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("server.Models.Domain.Room", b =>
                {
                    b.Navigation("Seats");
                });

            modelBuilder.Entity("server.Models.Domain.Seat", b =>
                {
                    b.Navigation("Bookings");
                });
#pragma warning restore 612, 618
        }
    }
}
