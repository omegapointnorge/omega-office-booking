﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using server.Context;

#nullable disable

namespace server.Migrations
{
    [DbContext(typeof(OfficeDbContext))]
    [Migration("20231109214420_initial")]
    partial class initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.13");

            modelBuilder.Entity("server.Data.BookingEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Bookingder")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("SeatId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("SeatId");

                    b.ToTable("Bookings");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Bookingder = "Sonia Reading",
                            SeatId = 1
                        },
                        new
                        {
                            Id = 2,
                            Bookingder = "Dick Johnson",
                            SeatId = 1
                        });
                });

            modelBuilder.Entity("server.Data.SeatEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Price")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Room")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Seats");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Price = 900000,
                            Room = "12"
                        },
                        new
                        {
                            Id = 2,
                            Price = 500000,
                            Room = "89"
                        },
                        new
                        {
                            Id = 3,
                            Price = 200500,
                            Room = "23"
                        });
                });

            modelBuilder.Entity("server.Data.BookingEntity", b =>
                {
                    b.HasOne("server.Data.SeatEntity", "Seat")
                        .WithMany()
                        .HasForeignKey("SeatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Seat");
                });
#pragma warning restore 612, 618
        }
    }
}
