using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeanScene1._1.Data.Migrations
{
    /// <inheritdoc />
    public partial class _1_12_2025 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Report_Reservations_ReservationsId",
                table: "Report");

            migrationBuilder.DropForeignKey(
                name: "FK_Report_SittingSchedules_SittingScheduleId",
                table: "Report");

            migrationBuilder.DropIndex(
                name: "IX_Report_ReservationsId",
                table: "Report");

            migrationBuilder.DropIndex(
                name: "IX_Report_SittingScheduleId",
                table: "Report");

            migrationBuilder.RenameColumn(
                name: "SittingScheduleId",
                table: "Report",
                newName: "TotalReservations");

            migrationBuilder.RenameColumn(
                name: "SittingId",
                table: "Report",
                newName: "PendingOrConfirmed");

            migrationBuilder.RenameColumn(
                name: "ReservationsId",
                table: "Report",
                newName: "Completed");

            migrationBuilder.RenameColumn(
                name: "ReservationId",
                table: "Report",
                newName: "Cancelled");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Report",
                newName: "ReportId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalReservations",
                table: "Report",
                newName: "SittingScheduleId");

            migrationBuilder.RenameColumn(
                name: "PendingOrConfirmed",
                table: "Report",
                newName: "SittingId");

            migrationBuilder.RenameColumn(
                name: "Completed",
                table: "Report",
                newName: "ReservationsId");

            migrationBuilder.RenameColumn(
                name: "Cancelled",
                table: "Report",
                newName: "ReservationId");

            migrationBuilder.RenameColumn(
                name: "ReportId",
                table: "Report",
                newName: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Report_ReservationsId",
                table: "Report",
                column: "ReservationsId");

            migrationBuilder.CreateIndex(
                name: "IX_Report_SittingScheduleId",
                table: "Report",
                column: "SittingScheduleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Report_Reservations_ReservationsId",
                table: "Report",
                column: "ReservationsId",
                principalTable: "Reservations",
                principalColumn: "ReservationsId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Report_SittingSchedules_SittingScheduleId",
                table: "Report",
                column: "SittingScheduleId",
                principalTable: "SittingSchedules",
                principalColumn: "SittingScheduleId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
