using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeanScene1._1.Data.Migrations
{
    /// <inheritdoc />
    public partial class beanscene : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SittingSchedules");

            migrationBuilder.AddColumn<int>(
                name: "Breakfast",
                table: "Report",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Dinner",
                table: "Report",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Lunch",
                table: "Report",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Breakfast",
                table: "Report");

            migrationBuilder.DropColumn(
                name: "Dinner",
                table: "Report");

            migrationBuilder.DropColumn(
                name: "Lunch",
                table: "Report");

            migrationBuilder.CreateTable(
                name: "SittingSchedules",
                columns: table => new
                {
                    SittingScheduleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Capasity = table.Column<int>(type: "int", nullable: false),
                    EndDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SittingSchedules", x => x.SittingScheduleId);
                });
        }
    }
}
