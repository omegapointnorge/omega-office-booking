using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace server.Migrations
{
    /// <inheritdoc />
    public partial class ConstraintForBookingDateAndSeatId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Bookings_SeatId",
                table: "Bookings");

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.AlterColumn<DateTime>(
                name: "BookingDateTime_DayOnly",
                table: "Bookings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "unique_seat_time_constraint",
                table: "Bookings",
                columns: new[] { "SeatId", "BookingDateTime_DayOnly" });

            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "Id", "BookingDateTime", "BookingDateTime_DayOnly", "SeatId", "UserId", "UserName" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 12, 7, 14, 44, 11, 768, DateTimeKind.Local).AddTicks(9580), new DateTime(2023, 12, 7, 0, 0, 0, 0, DateTimeKind.Local), 1, "860849a4-f4b8-4566-8ed1-918cf3d41a92", "SampleUser1" },
                    { 2, new DateTime(2023, 12, 5, 14, 44, 11, 768, DateTimeKind.Local).AddTicks(9580), new DateTime(2023, 12, 5, 0, 0, 0, 0, DateTimeKind.Local), 2, "639d660b-4724-407b-b05c-12b5f619f833", "SampleUser2" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "unique_seat_time_constraint",
                table: "Bookings");

            migrationBuilder.AlterColumn<DateTime>(
                name: "BookingDateTime_DayOnly",
                table: "Bookings",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 1,
                column: "BookingDateTime_DayOnly",
                value: null);

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 2,
                column: "BookingDateTime_DayOnly",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_SeatId",
                table: "Bookings",
                column: "SeatId");
        }
    }
}
