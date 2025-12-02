using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeanScene1._1.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddReservationType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ReservationType",
                table: "Reservations",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "OverdueReservations",
                table: "Report",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ThisMonthReservations",
                table: "Report",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ThisWeekReservations",
                table: "Report",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TodayReservations",
                table: "Report",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UpcomingReservations",
                table: "Report",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReservationType",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "OverdueReservations",
                table: "Report");

            migrationBuilder.DropColumn(
                name: "ThisMonthReservations",
                table: "Report");

            migrationBuilder.DropColumn(
                name: "ThisWeekReservations",
                table: "Report");

            migrationBuilder.DropColumn(
                name: "TodayReservations",
                table: "Report");

            migrationBuilder.DropColumn(
                name: "UpcomingReservations",
                table: "Report");
        }
    }
}
