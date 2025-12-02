using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeanScene1._1.Data.Migrations
{
    /// <inheritdoc />
    public partial class _28_11_2025_3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReservationTables_Reservations_ReservationsId",
                table: "ReservationTables");

            migrationBuilder.DropForeignKey(
                name: "FK_ReservationTables_Tables_TableId",
                table: "ReservationTables");

            migrationBuilder.DropIndex(
                name: "IX_ReservationTables_ReservationsId",
                table: "ReservationTables");

            migrationBuilder.DropIndex(
                name: "IX_ReservationTables_TableId",
                table: "ReservationTables");

            migrationBuilder.DropColumn(
                name: "ReservationsId",
                table: "ReservationTables");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationTables_ReservationId",
                table: "ReservationTables",
                column: "ReservationId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationTables_Reservations_ReservationId",
                table: "ReservationTables",
                column: "ReservationId",
                principalTable: "Reservations",
                principalColumn: "ReservationsId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationTables_Tables_TableId",
                table: "ReservationTables",
                column: "TableId",
                principalTable: "Tables",
                principalColumn: "TableId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReservationTables_Reservations_ReservationId",
                table: "ReservationTables");

            migrationBuilder.DropForeignKey(
                name: "FK_ReservationTables_Tables_TableId",
                table: "ReservationTables");

            migrationBuilder.DropIndex(
                name: "IX_ReservationTables_ReservationId",
                table: "ReservationTables");

            migrationBuilder.AddColumn<int>(
                name: "ReservationsId",
                table: "ReservationTables",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ReservationTables_ReservationsId",
                table: "ReservationTables",
                column: "ReservationsId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationTables_TableId",
                table: "ReservationTables",
                column: "TableId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationTables_Reservations_ReservationsId",
                table: "ReservationTables",
                column: "ReservationsId",
                principalTable: "Reservations",
                principalColumn: "ReservationsId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationTables_Tables_TableId",
                table: "ReservationTables",
                column: "TableId",
                principalTable: "Tables",
                principalColumn: "TableId");
        }
    }
}
