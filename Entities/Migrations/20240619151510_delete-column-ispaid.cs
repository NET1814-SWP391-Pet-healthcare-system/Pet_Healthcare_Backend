using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations
{
    /// <inheritdoc />
    public partial class deletecolumnispaid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isPaid",
                table: "Appointment");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isPaid",
                table: "Appointment",
                type: "bit",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Appointment",
                keyColumn: "AppointmentId",
                keyValue: 1,
                column: "isPaid",
                value: null);

            migrationBuilder.UpdateData(
                table: "Appointment",
                keyColumn: "AppointmentId",
                keyValue: 2,
                column: "isPaid",
                value: null);

            migrationBuilder.UpdateData(
                table: "Appointment",
                keyColumn: "AppointmentId",
                keyValue: 3,
                column: "isPaid",
                value: null);

            migrationBuilder.UpdateData(
                table: "Appointment",
                keyColumn: "AppointmentId",
                keyValue: 4,
                column: "isPaid",
                value: null);

            migrationBuilder.UpdateData(
                table: "Appointment",
                keyColumn: "AppointmentId",
                keyValue: 5,
                column: "isPaid",
                value: null);
        }
    }
}
