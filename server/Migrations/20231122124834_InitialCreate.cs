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
                value: new DateTime(2023, 11, 22, 13, 48, 34, 462, DateTimeKind.Local).AddTicks(9880));

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 2,
                column: "BookingDateTime",
                value: new DateTime(2023, 11, 23, 13, 48, 34, 462, DateTimeKind.Local).AddTicks(9910));

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 3,
                column: "BookingDateTime",
                value: new DateTime(2023, 11, 22, 15, 48, 34, 462, DateTimeKind.Local).AddTicks(9910));

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 4,
                column: "BookingDateTime",
                value: new DateTime(2023, 11, 24, 13, 48, 34, 462, DateTimeKind.Local).AddTicks(9910));

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 5,
                column: "BookingDateTime",
                value: new DateTime(2023, 11, 22, 16, 48, 34, 462, DateTimeKind.Local).AddTicks(9920));

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 6,
                column: "BookingDateTime",
                value: new DateTime(2023, 11, 25, 13, 48, 34, 462, DateTimeKind.Local).AddTicks(9920));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
