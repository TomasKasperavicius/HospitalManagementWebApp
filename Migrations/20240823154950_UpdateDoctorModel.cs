using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalManagementWebApp.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDoctorModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_workSchedules_doctors_DoctorID1",
                table: "workSchedules");

            migrationBuilder.DropIndex(
                name: "IX_workSchedules_DoctorID1",
                table: "workSchedules");

            migrationBuilder.DropColumn(
                name: "DoctorID1",
                table: "workSchedules");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DoctorID1",
                table: "workSchedules",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_workSchedules_DoctorID1",
                table: "workSchedules",
                column: "DoctorID1");

            migrationBuilder.AddForeignKey(
                name: "FK_workSchedules_doctors_DoctorID1",
                table: "workSchedules",
                column: "DoctorID1",
                principalTable: "doctors",
                principalColumn: "ID");
        }
    }
}
