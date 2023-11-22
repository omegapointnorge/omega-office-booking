using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace server.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 1,
                column: "BookingDateTime",
                value: new DateTime(2023, 11, 22, 13, 10, 17, 238, DateTimeKind.Local).AddTicks(9680));

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 2,
                column: "BookingDateTime",
                value: new DateTime(2023, 11, 23, 13, 10, 17, 238, DateTimeKind.Local).AddTicks(9720));

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 3,
                column: "BookingDateTime",
                value: new DateTime(2023, 11, 22, 15, 10, 17, 238, DateTimeKind.Local).AddTicks(9720));

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 4,
                column: "BookingDateTime",
                value: new DateTime(2023, 11, 24, 13, 10, 17, 238, DateTimeKind.Local).AddTicks(9720));

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 5,
                column: "BookingDateTime",
                value: new DateTime(2023, 11, 22, 16, 10, 17, 238, DateTimeKind.Local).AddTicks(9720));

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 6,
                column: "BookingDateTime",
                value: new DateTime(2023, 11, 25, 13, 10, 17, 238, DateTimeKind.Local).AddTicks(9720));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 1,
                column: "BookingDateTime",
                value: new DateTime(2023, 11, 22, 9, 54, 25, 600, DateTimeKind.Local).AddTicks(9330));

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 2,
                column: "BookingDateTime",
                value: new DateTime(2023, 11, 23, 9, 54, 25, 600, DateTimeKind.Local).AddTicks(9360));

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 3,
                column: "BookingDateTime",
                value: new DateTime(2023, 11, 22, 11, 54, 25, 600, DateTimeKind.Local).AddTicks(9360));

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 4,
                column: "BookingDateTime",
                value: new DateTime(2023, 11, 24, 9, 54, 25, 600, DateTimeKind.Local).AddTicks(9370));

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 5,
                column: "BookingDateTime",
                value: new DateTime(2023, 11, 22, 12, 54, 25, 600, DateTimeKind.Local).AddTicks(9370));

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 6,
                column: "BookingDateTime",
                value: new DateTime(2023, 11, 25, 9, 54, 25, 600, DateTimeKind.Local).AddTicks(9370));
        }
    }
}
