using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace server.Migrations
{
    /// <inheritdoc />
    public partial class SampledataBookingDataFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 2,
                column: "BookingDateTime",
                value: new DateTime(2023, 12, 5, 14, 44, 11, 768, DateTimeKind.Local).AddTicks(9580));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 2,
                column: "BookingDateTime",
                value: new DateTime(2023, 12, 8, 14, 44, 11, 768, DateTimeKind.Local).AddTicks(9641));
        }
    }
}
