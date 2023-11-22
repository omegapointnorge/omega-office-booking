using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace server.Migrations
{
    /// <inheritdoc />
    public partial class MovedSampleDataOutToSeperateClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 3, "Cache Corner" },
                    { 4, "Syntax Sanctuary" },
                    { 5, "Exception Escape" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Name", "PhoneNumber" },
                values: new object[,]
                {
                    { 3, "syntax_sorcerer@example.com", "Syntax Sorcerer", "" },
                    { 4, "pixel_picasso@example.com", "Pixel Picasso", "" },
                    { 5, "console_cowboy@example.com", "Console Cowboy", "" },
                    { 6, "bit_boffin@example.com", "Bit Boffin", "" }
                });

            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "Id", "BookingDateTime", "SeatId", "UserId" },
                values: new object[] { 3, new DateTime(2023, 11, 22, 11, 54, 25, 600, DateTimeKind.Local).AddTicks(9360), 3, 3 });

            migrationBuilder.InsertData(
                table: "Seats",
                columns: new[] { "Id", "RoomId" },
                values: new object[,]
                {
                    { 4, 3 },
                    { 5, 3 },
                    { 6, 4 },
                    { 7, 4 },
                    { 8, 5 },
                    { 9, 5 }
                });

            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "Id", "BookingDateTime", "SeatId", "UserId" },
                values: new object[,]
                {
                    { 4, new DateTime(2023, 11, 24, 9, 54, 25, 600, DateTimeKind.Local).AddTicks(9370), 4, 4 },
                    { 5, new DateTime(2023, 11, 22, 12, 54, 25, 600, DateTimeKind.Local).AddTicks(9370), 5, 5 },
                    { 6, new DateTime(2023, 11, 25, 9, 54, 25, 600, DateTimeKind.Local).AddTicks(9370), 6, 6 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 1,
                column: "BookingDateTime",
                value: new DateTime(2023, 11, 21, 16, 38, 14, 545, DateTimeKind.Local).AddTicks(6260));

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 2,
                column: "BookingDateTime",
                value: new DateTime(2023, 11, 22, 16, 38, 14, 545, DateTimeKind.Local).AddTicks(6280));
        }
    }
}
