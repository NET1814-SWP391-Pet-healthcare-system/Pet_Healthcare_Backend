using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PaymentStatus",
                table: "Appointment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    TransactionId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CustomerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AppointmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.TransactionId);
                    table.ForeignKey(
                        name: "FK_Transactions_Appointment_AppointmentId",
                        column: x => x.AppointmentId,
                        principalTable: "Appointment",
                        principalColumn: "AppointmentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transactions_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Appointment",
                keyColumn: "AppointmentId",
                keyValue: 1,
                column: "PaymentStatus",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Appointment",
                keyColumn: "AppointmentId",
                keyValue: 2,
                column: "PaymentStatus",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Appointment",
                keyColumn: "AppointmentId",
                keyValue: 3,
                column: "PaymentStatus",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Appointment",
                keyColumn: "AppointmentId",
                keyValue: 4,
                column: "PaymentStatus",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Appointment",
                keyColumn: "AppointmentId",
                keyValue: 5,
                column: "PaymentStatus",
                value: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_AppointmentId",
                table: "Transactions",
                column: "AppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_CustomerId",
                table: "Transactions",
                column: "CustomerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropColumn(
                name: "PaymentStatus",
                table: "Appointment");
        }
    }
}
