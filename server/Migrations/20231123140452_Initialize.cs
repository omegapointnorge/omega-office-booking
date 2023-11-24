using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace server.Migrations
{
    /// <inheritdoc />
    public partial class Initialize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 1,
                column: "BookingDateTime",
                value: new DateTime(2023, 11, 23, 15, 4, 52, 749, DateTimeKind.Local).AddTicks(8589));

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 2,
                column: "BookingDateTime",
                value: new DateTime(2023, 11, 24, 15, 4, 52, 749, DateTimeKind.Local).AddTicks(8645));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 1,
                column: "BookingDateTime",
                value: new DateTime(2023, 11, 23, 9, 49, 39, 392, DateTimeKind.Local).AddTicks(6750));

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 2,
                column: "BookingDateTime",
                value: new DateTime(2023, 11, 24, 9, 49, 39, 392, DateTimeKind.Local).AddTicks(6790));
        }
    }
}
