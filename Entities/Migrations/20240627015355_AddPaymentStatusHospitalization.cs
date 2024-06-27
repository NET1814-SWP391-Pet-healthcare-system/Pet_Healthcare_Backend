using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations
{
    /// <inheritdoc />
    public partial class AddPaymentStatusHospitalization : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PaymentStatus",
                table: "Hospitalization",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Hospitalization",
                keyColumn: "HospitalizationId",
                keyValue: 1,
                column: "PaymentStatus",
                value: null);

            migrationBuilder.UpdateData(
                table: "Hospitalization",
                keyColumn: "HospitalizationId",
                keyValue: 2,
                column: "PaymentStatus",
                value: null);

            migrationBuilder.UpdateData(
                table: "Hospitalization",
                keyColumn: "HospitalizationId",
                keyValue: 3,
                column: "PaymentStatus",
                value: null);

            migrationBuilder.UpdateData(
                table: "Hospitalization",
                keyColumn: "HospitalizationId",
                keyValue: 4,
                column: "PaymentStatus",
                value: null);

            migrationBuilder.UpdateData(
                table: "Hospitalization",
                keyColumn: "HospitalizationId",
                keyValue: 5,
                column: "PaymentStatus",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentStatus",
                table: "Hospitalization");
        }
    }
}
