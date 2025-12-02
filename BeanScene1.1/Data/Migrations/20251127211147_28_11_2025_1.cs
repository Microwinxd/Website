using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeanScene1._1.Data.Migrations
{
    /// <inheritdoc />
    public partial class _28_11_2025_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Report_Reservations_ReservationId",
                table: "Report");

            migrationBuilder.DropIndex(
                name: "IX_Report_ReservationId",
                table: "Report");

            migrationBuilder.AddColumn<int>(
                name: "ReservationsId",
                table: "Report",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Report_ReservationsId",
                table: "Report",
                column: "ReservationsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Report_Reservations_ReservationsId",
                table: "Report",
                column: "ReservationsId",
                principalTable: "Reservations",
                principalColumn: "ReservationsId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Report_Reservations_ReservationsId",
                table: "Report");

            migrationBuilder.DropIndex(
                name: "IX_Report_ReservationsId",
                table: "Report");

            migrationBuilder.DropColumn(
                name: "ReservationsId",
                table: "Report");

            migrationBuilder.CreateIndex(
                name: "IX_Report_ReservationId",
                table: "Report",
                column: "ReservationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Report_Reservations_ReservationId",
                table: "Report",
                column: "ReservationId",
                principalTable: "Reservations",
                principalColumn: "ReservationsId");
        }
    }
}
