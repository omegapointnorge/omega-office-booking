using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace server.Migrations
{
    /// <inheritdoc />
    public partial class NewFieldDateOnlyForBooking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "BookingDateTime_DayOnly",
                table: "Bookings",
                type: "datetime2",
                nullable: true,
                computedColumnSql: "CONVERT(date, [BookingDateTime])");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookingDateTime_DayOnly",
                table: "Bookings");
        }
    }
}