using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations
{
    /// <inheritdoc />
    public partial class AddIsDeletedUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

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

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "25e15040-df59-497a-8f8f-026f1dc4fab4",
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "283e003d-77ce-4e8a-876f-db63127169dc",
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2a4c598c-4a1b-452a-b2f2-3f0dc81c7a04",
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4f0f8727-ddeb-40b5-92d4-22200c4dbb3d",
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7e3fcb0f-11cd-4945-8d9a-84b6c717f94a",
                column: "IsDeleted",
                value: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "isPaid",
                table: "Appointment");
        }
    }
}
