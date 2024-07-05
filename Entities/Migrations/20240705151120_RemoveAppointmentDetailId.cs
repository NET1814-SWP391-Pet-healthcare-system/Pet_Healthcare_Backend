using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Entities.Migrations
{
    /// <inheritdoc />
    public partial class RemoveAppointmentDetailId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppointmentDetail_Appointment_AppointmentId",
                table: "AppointmentDetail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppointmentDetail",
                table: "AppointmentDetail");

            migrationBuilder.DropIndex(
                name: "IX_AppointmentDetail_AppointmentId",
                table: "AppointmentDetail");

            migrationBuilder.DeleteData(
                table: "AppointmentDetail",
                keyColumn: "AppointmentDetailId",
                keyColumnType: "int",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AppointmentDetail",
                keyColumn: "AppointmentDetailId",
                keyColumnType: "int",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AppointmentDetail",
                keyColumn: "AppointmentDetailId",
                keyColumnType: "int",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AppointmentDetail",
                keyColumn: "AppointmentDetailId",
                keyColumnType: "int",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "AppointmentDetail",
                keyColumn: "AppointmentDetailId",
                keyColumnType: "int",
                keyValue: 5);

            migrationBuilder.DropColumn(
                name: "AppointmentDetailId",
                table: "AppointmentDetail");

            migrationBuilder.AlterColumn<int>(
                name: "AppointmentId",
                table: "AppointmentDetail",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppointmentDetail",
                table: "AppointmentDetail",
                column: "AppointmentId");

            migrationBuilder.InsertData(
                table: "AppointmentDetail",
                columns: new[] { "AppointmentId", "Diagnosis", "Medication", "RecordId", "Treatment" },
                values: new object[,]
                {
                    { 1, "Healthy", null, 1, null },
                    { 2, "Ear Infection", "Otomax Otic Solution", 2, "Antibiotic Ear Drops" },
                    { 3, "Feather Plucking", null, 3, "Environmental Enrichment" },
                    { 4, "Swim Bladder Disorder", "Antibiotics and Anti-inflammatory", 4, "Medication and Diet Change" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_AppointmentDetail_Appointment_AppointmentId",
                table: "AppointmentDetail",
                column: "AppointmentId",
                principalTable: "Appointment",
                principalColumn: "AppointmentId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppointmentDetail_Appointment_AppointmentId",
                table: "AppointmentDetail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppointmentDetail",
                table: "AppointmentDetail");

            migrationBuilder.DeleteData(
                table: "AppointmentDetail",
                keyColumn: "AppointmentId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AppointmentDetail",
                keyColumn: "AppointmentId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AppointmentDetail",
                keyColumn: "AppointmentId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AppointmentDetail",
                keyColumn: "AppointmentId",
                keyValue: 4);

            migrationBuilder.AlterColumn<int>(
                name: "AppointmentId",
                table: "AppointmentDetail",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "AppointmentDetailId",
                table: "AppointmentDetail",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppointmentDetail",
                table: "AppointmentDetail",
                column: "AppointmentDetailId");

            migrationBuilder.InsertData(
                table: "AppointmentDetail",
                columns: new[] { "AppointmentDetailId", "AppointmentId", "Diagnosis", "Medication", "RecordId", "Treatment" },
                values: new object[,]
                {
                    { 1, 1, "Healthy", null, 1, null },
                    { 2, 2, "Ear Infection", "Otomax Otic Solution", 2, "Antibiotic Ear Drops" },
                    { 3, 3, "Feather Plucking", null, 3, "Environmental Enrichment" },
                    { 4, 4, "Swim Bladder Disorder", "Antibiotics and Anti-inflammatory", 4, "Medication and Diet Change" },
                    { 5, 5, "Gastrointestinal Stasis", "CisaprIde and Simethicone", 5, "Motility Medication and Massage" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentDetail_AppointmentId",
                table: "AppointmentDetail",
                column: "AppointmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppointmentDetail_Appointment_AppointmentId",
                table: "AppointmentDetail",
                column: "AppointmentId",
                principalTable: "Appointment",
                principalColumn: "AppointmentId");
        }
    }
}
