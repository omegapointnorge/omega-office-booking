﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using server.Context;

#nullable disable

namespace server.Migrations
{
    [DbContext(typeof(OfficeDbContext))]
    [Migration("20231123084939_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SeatId");

                    b.HasIndex("UserId");

                    b.ToTable("Bookings");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BookingDateTime = new DateTime(2023, 11, 23, 9, 49, 39, 392, DateTimeKind.Local).AddTicks(6750),
                            SeatId = 1,
                            UserId = 1
                        },
                        new
                        {
                            Id = 2,
                            BookingDateTime = new DateTime(2023, 11, 24, 9, 49, 39, 392, DateTimeKind.Local).AddTicks(6790),
                            SeatId = 2,
                            UserId = 2
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

                    b.Property<int>("RoomId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoomId");

                    b.ToTable("Seats");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            RoomId = 1
                        },
                        new
                        {
                            Id = 2,
                            RoomId = 1
                        },
                        new
                        {
                            Id = 3,
                            RoomId = 1
                        },
                        new
                        {
                            Id = 4,
                            RoomId = 1
                        },
                        new
                        {
                            Id = 5,
                            RoomId = 1
                        },
                        new
                        {
                            Id = 6,
                            RoomId = 1
                        },
                        new
                        {
                            Id = 7,
                            RoomId = 1
                        },
                        new
                        {
                            Id = 8,
                            RoomId = 1
                        },
                        new
                        {
                            Id = 9,
                            RoomId = 1
                        },
                        new
                        {
                            Id = 10,
                            RoomId = 1
                        },
                        new
                        {
                            Id = 11,
                            RoomId = 2
                        },
                        new
                        {
                            Id = 12,
                            RoomId = 2
                        },
                        new
                        {
                            Id = 13,
                            RoomId = 2
                        },
                        new
                        {
                            Id = 14,
                            RoomId = 2
                        },
                        new
                        {
                            Id = 15,
                            RoomId = 2
                        });
                });

            modelBuilder.Entity("server.Models.Domain.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "code_master@example.com",
                            Name = "Code Master Flex",
                            PhoneNumber = ""
                        },
                        new
                        {
                            Id = 2,
                            Email = "debug_diva@example.com",
                            Name = "Debug Diva",
                            PhoneNumber = ""
                        });
                });

            modelBuilder.Entity("server.Models.Domain.Booking", b =>
                {
                    b.HasOne("server.Models.Domain.Seat", null)
                        .WithMany("Bookings")
                        .HasForeignKey("SeatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("server.Models.Domain.User", null)
                        .WithMany("Bookings")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
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

            modelBuilder.Entity("server.Models.Domain.User", b =>
                {
                    b.Navigation("Bookings");
                });
#pragma warning restore 612, 618
        }
    }
}
