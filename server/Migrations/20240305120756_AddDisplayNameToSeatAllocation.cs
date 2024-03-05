using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace server.Migrations
{
    /// <inheritdoc />
    public partial class AddDisplayNameToSeatAllocation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DisplayName",
                table: "SeatAllocation",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "SeatAllocation",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DisplayName", "UserId" },
                values: new object[] { "Fornavn Etternavn", "7534c00c-79ee-4230-b721-8cd781c2212d" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisplayName",
                table: "SeatAllocation");

            migrationBuilder.UpdateData(
                table: "SeatAllocation",
                keyColumn: "Id",
                keyValue: 1,
                column: "UserId",
                value: "Salg test Johansen");
        }
    }
}
