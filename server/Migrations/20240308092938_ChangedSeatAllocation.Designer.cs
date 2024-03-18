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
    [Migration("20240308092938_ChangedSeatAllocation")]
    partial class ChangedSeatAllocation
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
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

                    b.Property<DateTime>("BookingDateTime_DayOnly")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<int?>("EventId")
                        .HasColumnType("int");

                    b.Property<int>("SeatId")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasAlternateKey("SeatId", "BookingDateTime_DayOnly")
                        .HasName("unique_seat_time_constraint");

                    b.HasIndex("EventId");

                    b.ToTable("Booking");
                });

            modelBuilder.Entity("server.Models.Domain.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Event");
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

                    b.ToTable("Room");

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
                        },
                        new
                        {
                            Id = 3,
                            Name = "Salg"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Marie"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Okonomi"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Oystein"
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

                    b.ToTable("Seat");

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
                        },
                        new
                        {
                            Id = 16,
                            IsAvailable = false,
                            RoomId = 3
                        },
                        new
                        {
                            Id = 17,
                            IsAvailable = true,
                            RoomId = 3
                        },
                        new
                        {
                            Id = 18,
                            IsAvailable = false,
                            RoomId = 3
                        },
                        new
                        {
                            Id = 19,
                            IsAvailable = false,
                            RoomId = 3
                        },
                        new
                        {
                            Id = 20,
                            IsAvailable = false,
                            RoomId = 3
                        },
                        new
                        {
                            Id = 21,
                            IsAvailable = false,
                            RoomId = 3
                        },
                        new
                        {
                            Id = 22,
                            IsAvailable = false,
                            RoomId = 3
                        },
                        new
                        {
                            Id = 23,
                            IsAvailable = true,
                            RoomId = 4
                        },
                        new
                        {
                            Id = 24,
                            IsAvailable = true,
                            RoomId = 4
                        },
                        new
                        {
                            Id = 25,
                            IsAvailable = true,
                            RoomId = 4
                        },
                        new
                        {
                            Id = 26,
                            IsAvailable = true,
                            RoomId = 5
                        },
                        new
                        {
                            Id = 27,
                            IsAvailable = true,
                            RoomId = 6
                        });
                });

            modelBuilder.Entity("server.Models.Domain.SeatAllocation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SeatId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasAlternateKey("SeatId")
                        .HasName("unique_seat_constraint");

                    b.ToTable("SeatAllocation");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "test@email.no",
                            SeatId = 2
                        });
                });

            modelBuilder.Entity("server.Models.Domain.Booking", b =>
                {
                    b.HasOne("server.Models.Domain.Event", "Event")
                        .WithMany("Bookings")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("server.Models.Domain.Seat", "Seat")
                        .WithMany("Bookings")
                        .HasForeignKey("SeatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Event");

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

            modelBuilder.Entity("server.Models.Domain.Event", b =>
                {
                    b.Navigation("Bookings");
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
