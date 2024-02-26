using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace server.Migrations
{
    /// <inheritdoc />
    public partial class AddCascadeDeleteToBookings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Event_EventId",
                table: "Bookings");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Event_EventId",
                table: "Bookings",
                column: "EventId",
                principalTable: "Event",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Event_EventId",
                table: "Bookings");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Event_EventId",
                table: "Bookings",
                column: "EventId",
                principalTable: "Event",
                principalColumn: "Id");
        }
    }
}
