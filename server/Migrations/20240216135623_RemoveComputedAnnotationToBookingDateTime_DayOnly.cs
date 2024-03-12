using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace server.Migrations
{
    /// <inheritdoc />
    public partial class RemoveComputedAnnotationToBookingDateTime_DayOnly : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "BookingDateTime_DayOnly",
                table: "Bookings",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldComputedColumnSql: "CONVERT(date, [BookingDateTime])");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "BookingDateTime_DayOnly",
                table: "Bookings",
                type: "datetime2",
                nullable: true,
                computedColumnSql: "CONVERT(date, [BookingDateTime])",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }
    }
}