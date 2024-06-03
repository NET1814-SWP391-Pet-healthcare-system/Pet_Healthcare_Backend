using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations
{
    /// <inheritdoc />
    public partial class FixAppointmentSampleData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Appointment",
                keyColumn: "AppointmentId",
                keyValue: 5,
                column: "TotalCost",
                value: 50.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Appointment",
                keyColumn: "AppointmentId",
                keyValue: 5,
                column: "TotalCost",
                value: 500.0);
        }
    }
}
