using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalManagementWebApp.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAppointmentModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_appointments_patients_UserID",
                table: "appointments");

            migrationBuilder.AlterColumn<int>(
                name: "UserID",
                table: "appointments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_appointments_patients_UserID",
                table: "appointments",
                column: "UserID",
                principalTable: "patients",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_appointments_patients_UserID",
                table: "appointments");

            migrationBuilder.AlterColumn<int>(
                name: "UserID",
                table: "appointments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_appointments_patients_UserID",
                table: "appointments",
                column: "UserID",
                principalTable: "patients",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
