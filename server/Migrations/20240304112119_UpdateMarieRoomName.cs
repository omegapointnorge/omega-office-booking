using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace server.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMarieRoomName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Marie");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "Id", "BookingDateTime", "BookingDateTime_DayOnly", "EventId", "SeatId", "UserId", "UserName" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 12, 7, 14, 44, 11, 768, DateTimeKind.Local), new DateTime(2023, 12, 7, 14, 44, 11, 768, DateTimeKind.Local), null, 1, "860849a4-f4b8-4566-8ed1-918cf3d41a92", "SampleUser1" },
                    { 2, new DateTime(2023, 12, 5, 14, 44, 11, 768, DateTimeKind.Local), new DateTime(2023, 12, 7, 14, 44, 11, 768, DateTimeKind.Local), null, 2, "639d660b-4724-407b-b05c-12b5f619f833", "SampleUser2" }
                });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Rekruttering");
        }
    }
}