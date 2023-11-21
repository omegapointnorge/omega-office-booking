using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace server.Migrations
{
    /// <inheritdoc />
    public partial class UppdatedModelsAndSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Office_OfficeId",
                table: "Rooms");

            migrationBuilder.DropTable(
                name: "Office");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_OfficeId",
                table: "Rooms");

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DropColumn(
                name: "OfficeId",
                table: "Rooms");

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
                columns: new[] { "BookingDateTime", "SeatId" },
                values: new object[] { new DateTime(2023, 11, 22, 16, 38, 14, 545, DateTimeKind.Local).AddTicks(6280), 2 });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Binary Bunker");

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "Algorithm Alcove" });

            migrationBuilder.UpdateData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: 3,
                column: "RoomId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Email", "Name" },
                values: new object[] { "code_master@example.com", "Code Master Flex" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Email", "Name" },
                values: new object[] { "debug_diva@example.com", "Debug Diva" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.AddColumn<int>(
                name: "OfficeId",
                table: "Rooms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Office",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Office", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 1,
                column: "BookingDateTime",
                value: new DateTime(2023, 11, 12, 12, 20, 48, 293, DateTimeKind.Local).AddTicks(9410));

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "BookingDateTime", "SeatId" },
                values: new object[] { new DateTime(2023, 11, 14, 12, 20, 48, 293, DateTimeKind.Local).AddTicks(9440), 5 });

            migrationBuilder.InsertData(
                table: "Office",
                columns: new[] { "Id", "Capacity", "Name" },
                values: new object[] { 1, 20, "Lille Grensen" });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Name", "OfficeId" },
                values: new object[] { "Store Rommet", 1 });

            migrationBuilder.UpdateData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: 3,
                column: "RoomId",
                value: 1);

            migrationBuilder.InsertData(
                table: "Seats",
                columns: new[] { "Id", "RoomId" },
                values: new object[,]
                {
                    { 4, 1 },
                    { 5, 1 }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Email", "Name" },
                values: new object[] { "Mostafa@omegapoint.no", "Mostafa" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Email", "Name" },
                values: new object[] { "Vicky@omegapoint.no", "Vicky" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Name", "PhoneNumber" },
                values: new object[] { 3, "Hakon@omegapoint.no", "Hakon", "" });

            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "Id", "BookingDateTime", "SeatId", "UserId" },
                values: new object[] { 3, new DateTime(2023, 11, 13, 12, 20, 48, 293, DateTimeKind.Local).AddTicks(9450), 2, 3 });

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_OfficeId",
                table: "Rooms",
                column: "OfficeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Office_OfficeId",
                table: "Rooms",
                column: "OfficeId",
                principalTable: "Office",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
