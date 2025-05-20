using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AvailabilitiesConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Availability_Schedule_ScheduleId",
                table: "Availability");

            migrationBuilder.DropTable(
                name: "Schedule");

            migrationBuilder.RenameColumn(
                name: "ScheduleId",
                table: "Availability",
                newName: "DoctorId");

            migrationBuilder.RenameColumn(
                name: "ScheduleDay",
                table: "Availability",
                newName: "DayOfWeek");

            migrationBuilder.RenameIndex(
                name: "IX_Availability_ScheduleId",
                table: "Availability",
                newName: "IX_Availability_DoctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Availability_User_DoctorId",
                table: "Availability",
                column: "DoctorId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Availability_User_DoctorId",
                table: "Availability");

            migrationBuilder.RenameColumn(
                name: "DoctorId",
                table: "Availability",
                newName: "ScheduleId");

            migrationBuilder.RenameColumn(
                name: "DayOfWeek",
                table: "Availability",
                newName: "ScheduleDay");

            migrationBuilder.RenameIndex(
                name: "IX_Availability_DoctorId",
                table: "Availability",
                newName: "IX_Availability_ScheduleId");

            migrationBuilder.CreateTable(
                name: "Schedule",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DoctorId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Schedule_User_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Schedule_DoctorId",
                table: "Schedule",
                column: "DoctorId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Availability_Schedule_ScheduleId",
                table: "Availability",
                column: "ScheduleId",
                principalTable: "Schedule",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
