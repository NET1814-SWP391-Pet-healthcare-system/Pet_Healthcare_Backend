﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Entities.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kennel",
                columns: table => new
                {
                    KennelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Capacity = table.Column<int>(type: "int", nullable: true),
                    DailyCost = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kennel", x => x.KennelId);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "Service",
                columns: table => new
                {
                    ServiceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cost = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Service", x => x.ServiceId);
                });

            migrationBuilder.CreateTable(
                name: "Slot",
                columns: table => new
                {
                    SlotId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartTime = table.Column<TimeOnly>(type: "time", nullable: true),
                    EndTime = table.Column<TimeOnly>(type: "time", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Slot", x => x.SlotId);
                });

            migrationBuilder.CreateTable(
                name: "Vaccine",
                columns: table => new
                {
                    VaccineId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsAnnualVaccine = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vaccine", x => x.VaccineId);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<bool>(type: "bit", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActice = table.Column<bool>(type: "bit", nullable: true),
                    Discriminator = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: true),
                    YearsOfExperience = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_User_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "RoleId");
                });

            migrationBuilder.CreateTable(
                name: "Pet",
                columns: table => new
                {
                    PetId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Species = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Breed = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<bool>(type: "bit", nullable: true),
                    Weight = table.Column<double>(type: "float", nullable: true),
                    ImageURL = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pet", x => x.PetId);
                    table.ForeignKey(
                        name: "FK_Pet_User_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "User",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "Appointment",
                columns: table => new
                {
                    AppointmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: true),
                    PetId = table.Column<int>(type: "int", nullable: true),
                    VetId = table.Column<int>(type: "int", nullable: true),
                    SlotId = table.Column<int>(type: "int", nullable: true),
                    ServiceId = table.Column<int>(type: "int", nullable: true),
                    Date = table.Column<DateOnly>(type: "date", nullable: true),
                    TotalCost = table.Column<double>(type: "float", nullable: true),
                    CancellationDate = table.Column<DateOnly>(type: "date", nullable: true),
                    RefundAmount = table.Column<double>(type: "float", nullable: true),
                    Rating = table.Column<int>(type: "int", nullable: true),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointment", x => x.AppointmentId);
                    table.ForeignKey(
                        name: "FK_Appointment_Pet_PetId",
                        column: x => x.PetId,
                        principalTable: "Pet",
                        principalColumn: "PetId");
                    table.ForeignKey(
                        name: "FK_Appointment_Service_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Service",
                        principalColumn: "ServiceId");
                    table.ForeignKey(
                        name: "FK_Appointment_Slot_SlotId",
                        column: x => x.SlotId,
                        principalTable: "Slot",
                        principalColumn: "SlotId");
                    table.ForeignKey(
                        name: "FK_Appointment_User_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Appointment_User_VetId",
                        column: x => x.VetId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Hospitalization",
                columns: table => new
                {
                    HospitalizationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PetId = table.Column<int>(type: "int", nullable: true),
                    KennelId = table.Column<int>(type: "int", nullable: true),
                    VetId = table.Column<int>(type: "int", nullable: true),
                    AdmissionDate = table.Column<DateOnly>(type: "date", nullable: true),
                    DischargeDate = table.Column<DateOnly>(type: "date", nullable: true),
                    TotalCost = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hospitalization", x => x.HospitalizationId);
                    table.ForeignKey(
                        name: "FK_Hospitalization_Kennel_KennelId",
                        column: x => x.KennelId,
                        principalTable: "Kennel",
                        principalColumn: "KennelId");
                    table.ForeignKey(
                        name: "FK_Hospitalization_Pet_PetId",
                        column: x => x.PetId,
                        principalTable: "Pet",
                        principalColumn: "PetId");
                    table.ForeignKey(
                        name: "FK_Hospitalization_User_VetId",
                        column: x => x.VetId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PetVaccination",
                columns: table => new
                {
                    PetId = table.Column<int>(type: "int", nullable: false),
                    VaccineId = table.Column<int>(type: "int", nullable: false),
                    VaccinationDate = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PetVaccination", x => new { x.PetId, x.VaccineId });
                    table.ForeignKey(
                        name: "FK_PetVaccination_Pet_PetId",
                        column: x => x.PetId,
                        principalTable: "Pet",
                        principalColumn: "PetId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PetVaccination_Vaccine_VaccineId",
                        column: x => x.VaccineId,
                        principalTable: "Vaccine",
                        principalColumn: "VaccineId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Record",
                columns: table => new
                {
                    RecordId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PetId = table.Column<int>(type: "int", nullable: true),
                    NumberOfVisits = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Record", x => x.RecordId);
                    table.ForeignKey(
                        name: "FK_Record_Pet_PetId",
                        column: x => x.PetId,
                        principalTable: "Pet",
                        principalColumn: "PetId");
                });

            migrationBuilder.CreateTable(
                name: "PetHealthTrack",
                columns: table => new
                {
                    PetHealthTrackId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HospitalizationId = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOnly = table.Column<DateOnly>(type: "date", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PetHealthTrack", x => x.PetHealthTrackId);
                    table.ForeignKey(
                        name: "FK_PetHealthTrack_Hospitalization_HospitalizationId",
                        column: x => x.HospitalizationId,
                        principalTable: "Hospitalization",
                        principalColumn: "HospitalizationId");
                });

            migrationBuilder.CreateTable(
                name: "AppointmentDetail",
                columns: table => new
                {
                    AppointmentDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppointmentId = table.Column<int>(type: "int", nullable: true),
                    RecordId = table.Column<int>(type: "int", nullable: true),
                    Diagnosis = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Treatment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Medication = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppointmentDetail", x => x.AppointmentDetailId);
                    table.ForeignKey(
                        name: "FK_AppointmentDetail_Appointment_AppointmentId",
                        column: x => x.AppointmentId,
                        principalTable: "Appointment",
                        principalColumn: "AppointmentId");
                    table.ForeignKey(
                        name: "FK_AppointmentDetail_Record_RecordId",
                        column: x => x.RecordId,
                        principalTable: "Record",
                        principalColumn: "RecordId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Kennel",
                columns: new[] { "KennelId", "Capacity", "DailyCost", "Description" },
                values: new object[,]
                {
                    { 1, 20, 25.0, "Comfortable and secure kennel for your pet." },
                    { 2, 10, 40.0, "Luxury kennel with premium amenities." },
                    { 3, 30, 15.0, "Budget-friendly kennel for short-term stays." },
                    { 4, 25, 30.0, "Large kennel with outdoor play area." },
                    { 5, 15, 35.0, "Climate-controlled kennel for exotic pets." }
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "RoleId", "Name" },
                values: new object[,]
                {
                    { 1, "Admin" },
                    { 2, "Customer" },
                    { 3, "Veterinarian" },
                    { 4, "Employee" }
                });

            migrationBuilder.InsertData(
                table: "Service",
                columns: new[] { "ServiceId", "Cost", "Description", "Name" },
                values: new object[,]
                {
                    { 1, 50.0, "Comprehensive physical examination for your pet.", "Annual Checkup" },
                    { 2, 30.0, "Administering essential vaccinations for your pet.", "Vaccination" },
                    { 3, 40.0, "Professional grooming services for your pet.", "Grooming" },
                    { 4, 75.0, "Thorough dental cleaning and oral health check.", "Dental Cleaning" },
                    { 5, 500.0, "Surgical procedures for your pet.", "Surgery" }
                });

            migrationBuilder.InsertData(
                table: "Slot",
                columns: new[] { "SlotId", "EndTime", "StartTime" },
                values: new object[,]
                {
                    { 1, new TimeOnly(10, 0, 0), new TimeOnly(9, 0, 0) },
                    { 2, new TimeOnly(12, 0, 0), new TimeOnly(11, 0, 0) },
                    { 3, new TimeOnly(15, 0, 0), new TimeOnly(14, 0, 0) },
                    { 4, new TimeOnly(17, 0, 0), new TimeOnly(16, 0, 0) },
                    { 5, new TimeOnly(19, 0, 0), new TimeOnly(18, 0, 0) }
                });

            migrationBuilder.InsertData(
                table: "Vaccine",
                columns: new[] { "VaccineId", "Description", "IsAnnualVaccine", "Name" },
                values: new object[,]
                {
                    { 1, "Protects against the rabies virus.", true, "Rabies Vaccine" },
                    { 2, "Protects against canine distemper virus.", false, "Distemper Vaccine" },
                    { 3, "Protects against feline leukemia virus.", true, "Feline Leukemia Vaccine" },
                    { 4, "Protects against kennel cough in dogs.", false, "Bordetella Vaccine" },
                    { 5, "Protects against parvovirus in dogs.", false, "Parvo Vaccine" }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "UserId", "Address", "Country", "Discriminator", "Email", "FirstName", "Gender", "ImageURL", "IsActice", "LastName", "Password", "RoleId", "Username" },
                values: new object[,]
                {
                    { 1, "123 Main St, Anytown USA", "United States", "User", "john.doe@example.com", "John", true, null, null, "Doe", "password123", 2, "jdoe" },
                    { 2, "456 Oak Rd, Anytown USA", "United States", "User", "jane.smith@example.com", "Jane", false, null, null, "Smith", "password456", 2, "jsmith" },
                    { 3, "789 Elm St, Anytown USA", "United States", "User", "bob.johnson@example.com", "Bob", true, null, null, "Johnson", "password789", 1, "bjohnson" },
                    { 4, "456 Pine Ave, Anytown USA", "United States", "User", "emily.wilson@example.com", "Emily", false, null, null, "Wilson", "password321", 3, "ewilson" },
                    { 5, "789 Maple Ln, Anytown USA", "United States", "User", "michael.brown@example.com", "Michael", true, null, null, "Brown", "password654", 3, "mbrown" }
                });

            migrationBuilder.InsertData(
                table: "Pet",
                columns: new[] { "PetId", "Breed", "CustomerId", "Gender", "ImageURL", "Name", "Species", "Weight" },
                values: new object[,]
                {
                    { 1, "Labrador Retriever", 1, true, null, "Buddy", "Dog", 30.5 },
                    { 2, "Siamese", 2, false, null, "Whiskers", "Cat", 4.2000000000000002 },
                    { 3, "Cockatiel", 3, true, null, "Rocky", "Bird", 0.29999999999999999 },
                    { 4, "Goldfish", 4, false, null, "Finny", "Fish", 0.10000000000000001 },
                    { 5, "Lop", 5, true, null, "Fluffy", "Rabbit", 2.7999999999999998 }
                });

            migrationBuilder.InsertData(
                table: "Appointment",
                columns: new[] { "AppointmentId", "CancellationDate", "Comments", "CustomerId", "Date", "PetId", "Rating", "RefundAmount", "ServiceId", "SlotId", "Status", "TotalCost", "VetId" },
                values: new object[,]
                {
                    { 1, null, null, 1, new DateOnly(2023, 6, 15), 1, null, null, 1, 1, 1, 75.0, 1 },
                    { 2, null, null, 2, new DateOnly(2023, 7, 1), 2, null, null, 2, 2, 2, 60.0, 2 },
                    { 3, new DateOnly(2023, 8, 8), "Friendly staff, great service.", 3, new DateOnly(2023, 8, 10), 3, 4, 20.0, 3, 3, 3, 40.0, 1 },
                    { 4, null, null, 4, new DateOnly(2023, 9, 1), 4, null, null, 4, 4, 1, 35.0, 2 },
                    { 5, null, null, 5, new DateOnly(2023, 10, 15), 5, null, null, 1, 5, 2, 50.0, 1 }
                });

            migrationBuilder.InsertData(
                table: "Hospitalization",
                columns: new[] { "HospitalizationId", "AdmissionDate", "DischargeDate", "KennelId", "PetId", "TotalCost", "VetId" },
                values: new object[,]
                {
                    { 1, new DateOnly(2023, 5, 1), new DateOnly(2023, 5, 5), 1, 1, 500.0, 1 },
                    { 2, new DateOnly(2023, 6, 10), new DateOnly(2023, 6, 12), 2, 2, 300.0, 2 },
                    { 3, new DateOnly(2023, 7, 15), new DateOnly(2023, 7, 18), 3, 3, 150.0, 1 },
                    { 4, new DateOnly(2023, 8, 1), new DateOnly(2023, 8, 3), 4, 4, 200.0, 2 },
                    { 5, new DateOnly(2023, 9, 10), new DateOnly(2023, 9, 12), 5, 5, 250.0, 1 }
                });

            migrationBuilder.InsertData(
                table: "PetVaccination",
                columns: new[] { "PetId", "VaccineId", "VaccinationDate" },
                values: new object[,]
                {
                    { 1, 1, new DateOnly(2023, 4, 1) },
                    { 1, 2, new DateOnly(2022, 8, 15) },
                    { 1, 4, new DateOnly(2023, 1, 10) },
                    { 2, 3, new DateOnly(2023, 3, 1) },
                    { 3, 5, new DateOnly(2023, 2, 20) }
                });

            migrationBuilder.InsertData(
                table: "Record",
                columns: new[] { "RecordId", "NumberOfVisits", "PetId" },
                values: new object[,]
                {
                    { 1, 5, 1 },
                    { 2, 3, 2 },
                    { 3, 2, 3 },
                    { 4, 1, 4 },
                    { 5, 4, 5 }
                });

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

            migrationBuilder.InsertData(
                table: "PetHealthTrack",
                columns: new[] { "PetHealthTrackId", "DateOnly", "Description", "HospitalizationId", "Status" },
                values: new object[,]
                {
                    { 1, null, "Recovering from surgery", 1, 1 },
                    { 2, null, "Monitoring for dehydration", 2, 2 },
                    { 3, null, "Treating respiratory infection", 3, 3 },
                    { 4, null, "Observing swim bladder disorder", 4, 1 },
                    { 5, null, "Managing gastrointestinal stasis", 5, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_CustomerId",
                table: "Appointment",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_PetId",
                table: "Appointment",
                column: "PetId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_ServiceId",
                table: "Appointment",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_SlotId",
                table: "Appointment",
                column: "SlotId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_VetId",
                table: "Appointment",
                column: "VetId");

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentDetail_AppointmentId",
                table: "AppointmentDetail",
                column: "AppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentDetail_RecordId",
                table: "AppointmentDetail",
                column: "RecordId");

            migrationBuilder.CreateIndex(
                name: "IX_Hospitalization_KennelId",
                table: "Hospitalization",
                column: "KennelId");

            migrationBuilder.CreateIndex(
                name: "IX_Hospitalization_PetId",
                table: "Hospitalization",
                column: "PetId");

            migrationBuilder.CreateIndex(
                name: "IX_Hospitalization_VetId",
                table: "Hospitalization",
                column: "VetId");

            migrationBuilder.CreateIndex(
                name: "IX_Pet_CustomerId",
                table: "Pet",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_PetHealthTrack_HospitalizationId",
                table: "PetHealthTrack",
                column: "HospitalizationId");

            migrationBuilder.CreateIndex(
                name: "IX_PetVaccination_VaccineId",
                table: "PetVaccination",
                column: "VaccineId");

            migrationBuilder.CreateIndex(
                name: "IX_Record_PetId",
                table: "Record",
                column: "PetId");

            migrationBuilder.CreateIndex(
                name: "IX_User_RoleId",
                table: "User",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppointmentDetail");

            migrationBuilder.DropTable(
                name: "PetHealthTrack");

            migrationBuilder.DropTable(
                name: "PetVaccination");

            migrationBuilder.DropTable(
                name: "Appointment");

            migrationBuilder.DropTable(
                name: "Record");

            migrationBuilder.DropTable(
                name: "Hospitalization");

            migrationBuilder.DropTable(
                name: "Vaccine");

            migrationBuilder.DropTable(
                name: "Service");

            migrationBuilder.DropTable(
                name: "Slot");

            migrationBuilder.DropTable(
                name: "Kennel");

            migrationBuilder.DropTable(
                name: "Pet");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Role");
        }
    }
}
