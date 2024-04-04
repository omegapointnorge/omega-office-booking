using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace server.Migrations
{
    /// <inheritdoc />
    public partial class AddClassJobExecutionLogEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JobExecutionLog",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExecutionTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ErrorMessage = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobExecutionLog", x => x.ID);
                });

            migrationBuilder.UpdateData(
                table: "SeatAllocation",
                keyColumn: "Id",
                keyValue: 1,
                column: "Email",
                value: "eric.bondoux@omegapoint.no");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobExecutionLog");

            migrationBuilder.UpdateData(
                table: "SeatAllocation",
                keyColumn: "Id",
                keyValue: 1,
                column: "Email",
                value: "test@email.no");
        }
    }
}
