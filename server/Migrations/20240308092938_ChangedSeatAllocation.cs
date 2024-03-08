using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace server.Migrations
{
    /// <inheritdoc />
    public partial class ChangedSeatAllocation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SeatAllocation",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.InsertData(
                table: "SeatAllocation",
                columns: new[] { "Id", "Email", "SeatId" },
                values: new object[] { 1, "test@email.no", 2 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SeatAllocation",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.InsertData(
                table: "SeatAllocation",
                columns: new[] { "Id", "Email", "SeatId" },
                values: new object[] { 1, "Salg test Johansen", 1 });
        }
    }
}
