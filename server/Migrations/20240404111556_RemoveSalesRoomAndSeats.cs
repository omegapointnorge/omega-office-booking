using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace server.Migrations
{
    /// <inheritdoc />
    public partial class RemoveSalesRoomAndSeats : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DeleteData(
                    table: "Seat",
                    keyColumn: "RoomId",
                    keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Room",
                keyColumn: "Id",
                keyValue: 3);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Room",
                columns: new [] { "Id", "Name" },
                values: new object[] {3, "Salg"});

            migrationBuilder.InsertData(
                table: "Seat",
                columns: new[] { "Id", "IsAvailable", "RoomId" },
                values: new object[,]
                {
                    { 16, false, 3 },
                    { 17, true, 3 },
                    { 18, false, 3 },
                    { 19, false, 3 },
                    { 20, false, 3 },
                    { 21, false, 3 },
                    { 22, false, 3 }
                });
        }
    }
}
