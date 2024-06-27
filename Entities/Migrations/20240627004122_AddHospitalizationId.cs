using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations
{
    /// <inheritdoc />
    public partial class AddHospitalizationId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HospitalizationId",
                table: "Transaction",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_HospitalizationId",
                table: "Transaction",
                column: "HospitalizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Hospitalization_HospitalizationId",
                table: "Transaction",
                column: "HospitalizationId",
                principalTable: "Hospitalization",
                principalColumn: "HospitalizationId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Hospitalization_HospitalizationId",
                table: "Transaction");

            migrationBuilder.DropIndex(
                name: "IX_Transaction_HospitalizationId",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "HospitalizationId",
                table: "Transaction");
        }
    }
}
