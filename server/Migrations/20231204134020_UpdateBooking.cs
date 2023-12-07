using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace server.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBooking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "BookingDateTime", "UserId" },
                values: new object[] { new DateTime(2023, 12, 4, 14, 40, 20, 577, DateTimeKind.Local).AddTicks(759), 2 });

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "BookingDateTime", "UserId" },
                values: new object[] { new DateTime(2023, 12, 4, 14, 40, 20, 577, DateTimeKind.Local).AddTicks(841), 3 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "BookingDateTime", "UserId" },
                values: new object[] { new DateTime(2023, 11, 23, 9, 49, 39, 392, DateTimeKind.Local).AddTicks(6750), 1 });

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "BookingDateTime", "UserId" },
                values: new object[] { new DateTime(2023, 11, 24, 9, 49, 39, 392, DateTimeKind.Local).AddTicks(6790), 2 });
        }
    }
}
