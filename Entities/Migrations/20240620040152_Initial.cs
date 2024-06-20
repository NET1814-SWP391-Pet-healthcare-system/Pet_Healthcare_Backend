using System;
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
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<bool>(type: "bit", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefreshTokenExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Kennel",
                columns: table => new
                {
                    KennelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    DailyCost = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kennel", x => x.KennelId);
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
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customer_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vet",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: true),
                    YearsOfExperience = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vet_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pet",
                columns: table => new
                {
                    PetId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<string>(type: "nvarchar(450)", nullable: true),
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
                        name: "FK_Pet_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Appointment",
                columns: table => new
                {
                    AppointmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PetId = table.Column<int>(type: "int", nullable: true),
                    VetId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SlotId = table.Column<int>(type: "int", nullable: true),
                    ServiceId = table.Column<int>(type: "int", nullable: true),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    TotalCost = table.Column<double>(type: "float", nullable: false),
                    CancellationDate = table.Column<DateOnly>(type: "date", nullable: true),
                    RefundAmount = table.Column<double>(type: "float", nullable: true),
                    Rating = table.Column<int>(type: "int", nullable: true),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true),
                    PaymentStatus = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointment", x => x.AppointmentId);
                    table.ForeignKey(
                        name: "FK_Appointment_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                        name: "FK_Appointment_Vet_VetId",
                        column: x => x.VetId,
                        principalTable: "Vet",
                        principalColumn: "Id",
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
                    VetId = table.Column<string>(type: "nvarchar(450)", nullable: true),
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
                        name: "FK_Hospitalization_Vet_VetId",
                        column: x => x.VetId,
                        principalTable: "Vet",
                        principalColumn: "Id",
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
                name: "Transaction",
                columns: table => new
                {
                    TransactionId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CustomerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AppointmentId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.TransactionId);
                    table.ForeignKey(
                        name: "FK_Transaction_Appointment_AppointmentId",
                        column: x => x.AppointmentId,
                        principalTable: "Appointment",
                        principalColumn: "AppointmentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transaction_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PetHealthTrack",
                columns: table => new
                {
                    PetHealthTrackId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HospitalizationId = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateOnly>(type: "date", nullable: true),
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
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3f5e4236-c7ce-4c0b-b352-05c6c82bb630", null, "Customer", "CUSTOMER" },
                    { "60edf088-fd2c-47c4-a07c-1f583015ac43", null, "Vet", "VET" },
                    { "b90340c2-bdef-4788-95ef-5b147ed6b337", null, "Employee", "EMPLOYEE" },
                    { "bbcac0cf-bc13-43b1-9b44-e2978916acac", null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "Country", "Email", "EmailConfirmed", "FirstName", "Gender", "ImageURL", "IsActive", "IsDeleted", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RefreshToken", "RefreshTokenExpiryDate", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "25e15040-df59-497a-8f8f-026f1dc4fab4", 0, "789 Elm St, Anytown USA", "82878f4d-de40-4299-a13b-31ef4b055113", "United States", "admin@example.com", false, "Bob", true, "https://example.com/user_images/bjohnson.jpg", true, false, "Johnson", false, null, "ADMIN@EXAMPLE.COM", "BJOHNSON", "AQAAAAIAAYagAAAAEG7krGqAQBUa5JwXbd9rzFfXZsWNYukCWWgb03qhERwegVVN6laGSAd7vnvEdhlkFw==", null, false, null, null, "6CMO6VX3FO3WWXMC77BFPOFRWCMNYZB7", false, "bjohnson" },
                    { "283e003d-77ce-4e8a-876f-db63127169dc", 0, "123 Main St, Anytown USA", "80e8da68-df49-4369-9582-72e9aa38ed30", "United States", "customer1@example.com", false, "John", true, "https://example.com/user_images/jdoe.jpg", true, false, "Doe", false, null, "CUSTOMER1@EXAMPLE.COM", "JDOE", "AQAAAAIAAYagAAAAEBa075/ZkJdl7Ut+by7+jhqVt3TGqvEmhNNplk3mJ7rN0baZuhEjWlRdDhwUMFvj8A==", null, false, null, null, "JSVRFDP6WGGEWI5JH42L2ZHKAYOH426U", false, "jdoe" },
                    { "2a4c598c-4a1b-452a-b2f2-3f0dc81c7a04", 0, "456 Oak Rd, Anytown USA", "0a504d88-ab5b-47dd-8575-69d47d267b4f", "United States", "customer2@example.com", false, "Jane", false, "https://example.com/user_images/jsmith.jpg", true, false, "Smith", false, null, "CUSTOMER2@EXAMPLE.COM", "JSMITH", "AQAAAAIAAYagAAAAEMBeygPd7Wknza3pycDGO/6y1APxstU8TjpDHt6eu5Zem1X2Y95uGTBjZ5cJux8zsA==", null, false, null, null, "43S2GHAER56TVFDSZOZAYI2Z7E75KTKI", false, "jsmith" },
                    { "4f0f8727-ddeb-40b5-92d4-22200c4dbb3d", 0, "789 Maple Ln, Anytown USA", "f550fda2-27ce-4fe5-a611-9d211f2dbf2c", "United States", "vet2@example.com", false, "Michael", true, "https://example.com/user_images/mbrown.jpg", true, false, "Brown", false, null, "VET2@EXAMPLE.COM", "MBROWN", "AQAAAAIAAYagAAAAEG7krGqAQBUa5JwXbd9rzFfXZsWNYukCWWgb03qhERwegVVN6laGSAd7vnvEdhlkFw==", null, false, null, null, "XUEHTI5VWZNP55F5J57ZWHDNFY4HJ2HF", false, "mbrown" },
                    { "7e3fcb0f-11cd-4945-8d9a-84b6c717f94a", 0, "456 Pine Ave, Anytown USA", "572bfe5b-9699-4cd2-9b06-e967437496a9", "United States", "vet1@example.com", false, "Emily", false, "https://example.com/user_images/ewilson.jpg", true, false, "Wilson", false, null, "VET1@EXAMPLE.COM", "EWILSON", "AQAAAAIAAYagAAAAEG7krGqAQBUa5JwXbd9rzFfXZsWNYukCWWgb03qhERwegVVN6laGSAd7vnvEdhlkFw==", null, false, null, null, "4FXXXACBIUMVV3CQNXAFEGWGAYVAK77O", false, "ewilson" }
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
                table: "Service",
                columns: new[] { "ServiceId", "Cost", "Description", "Name" },
                values: new object[,]
                {
                    { 1, 50.0, "Comprehensive health check-up for your pet.", "Annual Check-Up" },
                    { 2, 30.0, "Essential vaccines to keep your pet healthy.", "Vaccination" },
                    { 3, 40.0, "Addressing behavioral issues with your pet.", "Behavioral Consultation" },
                    { 4, 75.0, "Advanced surgical procedures for complex conditions.", "Specialized Surgery" }
                });

            migrationBuilder.InsertData(
                table: "Slot",
                columns: new[] { "SlotId", "EndTime", "StartTime" },
                values: new object[,]
                {
                    { 1, new TimeOnly(10, 0, 0), new TimeOnly(9, 0, 0) },
                    { 2, new TimeOnly(11, 0, 0), new TimeOnly(10, 0, 0) },
                    { 3, new TimeOnly(12, 0, 0), new TimeOnly(11, 0, 0) },
                    { 4, new TimeOnly(13, 0, 0), new TimeOnly(12, 0, 0) },
                    { 5, new TimeOnly(14, 0, 0), new TimeOnly(13, 0, 0) },
                    { 6, new TimeOnly(15, 0, 0), new TimeOnly(14, 0, 0) },
                    { 7, new TimeOnly(16, 0, 0), new TimeOnly(15, 0, 0) }
                });

            migrationBuilder.InsertData(
                table: "Vaccine",
                columns: new[] { "VaccineId", "Description", "IsAnnualVaccine", "Name" },
                values: new object[,]
                {
                    { 1, "Prevents rabies infection.", null, "Rabies Vaccine" },
                    { 2, "Prevents canine distemper.", null, "Distemper Vaccine" },
                    { 3, "Prevents feline leukemia.", null, "Feline Leukemia Vaccine" },
                    { 4, "Prevents canine parvovirus.", null, "Parvovirus Vaccine" },
                    { 5, "Prevents avian influenza.", null, "Avian Influenza Vaccine" }
                });

            migrationBuilder.InsertData(
                table: "Customer",
                column: "Id",
                values: new object[]
                {
                    "283e003d-77ce-4e8a-876f-db63127169dc",
                    "2a4c598c-4a1b-452a-b2f2-3f0dc81c7a04"
                });

            migrationBuilder.InsertData(
                table: "Vet",
                columns: new[] { "Id", "Rating", "YearsOfExperience" },
                values: new object[,]
                {
                    { "4f0f8727-ddeb-40b5-92d4-22200c4dbb3d", 5, 7 },
                    { "7e3fcb0f-11cd-4945-8d9a-84b6c717f94a", 4, 10 }
                });

            migrationBuilder.InsertData(
                table: "Pet",
                columns: new[] { "PetId", "Breed", "CustomerId", "Gender", "ImageURL", "Name", "Species", "Weight" },
                values: new object[,]
                {
                    { 1, "Labrador Retriever", "283e003d-77ce-4e8a-876f-db63127169dc", true, "https://example.com/pet_images/buddy.jpg", "Buddy", "Dog", 30.5 },
                    { 2, "Siamese", "2a4c598c-4a1b-452a-b2f2-3f0dc81c7a04", false, "https://example.com/pet_images/whiskers.jpg", "Whiskers", "Cat", 4.2000000000000002 },
                    { 3, "Cockatiel", "2a4c598c-4a1b-452a-b2f2-3f0dc81c7a04", true, "https://example.com/pet_images/rocky.jpg", "Rocky", "Bird", 0.29999999999999999 },
                    { 4, "Goldfish", "283e003d-77ce-4e8a-876f-db63127169dc", false, "https://example.com/pet_images/finny.jpg", "Finny", "Fish", 0.10000000000000001 },
                    { 5, "Lop", "283e003d-77ce-4e8a-876f-db63127169dc", true, "https://example.com/pet_images/fluffy.jpg", "Fluffy", "Rabbit", 2.7999999999999998 }
                });

            migrationBuilder.InsertData(
                table: "Appointment",
                columns: new[] { "AppointmentId", "CancellationDate", "Comments", "CustomerId", "Date", "PaymentStatus", "PetId", "Rating", "RefundAmount", "ServiceId", "SlotId", "Status", "TotalCost", "VetId" },
                values: new object[,]
                {
                    { 1, null, null, "283e003d-77ce-4e8a-876f-db63127169dc", new DateOnly(2023, 6, 15), null, 1, null, null, 1, 1, 0, 50.0, "7e3fcb0f-11cd-4945-8d9a-84b6c717f94a" },
                    { 2, null, null, "2a4c598c-4a1b-452a-b2f2-3f0dc81c7a04", new DateOnly(2023, 7, 1), null, 2, null, null, 2, 2, 1, 30.0, "7e3fcb0f-11cd-4945-8d9a-84b6c717f94a" },
                    { 3, null, "Friendly staff, great service.", "2a4c598c-4a1b-452a-b2f2-3f0dc81c7a04", new DateOnly(2023, 8, 10), null, 3, 4, null, 3, 3, 2, 40.0, "4f0f8727-ddeb-40b5-92d4-22200c4dbb3d" },
                    { 4, null, null, "283e003d-77ce-4e8a-876f-db63127169dc", new DateOnly(2023, 9, 1), null, 4, null, null, 4, 4, 0, 75.0, "4f0f8727-ddeb-40b5-92d4-22200c4dbb3d" },
                    { 5, null, null, "283e003d-77ce-4e8a-876f-db63127169dc", new DateOnly(2023, 10, 15), null, 5, null, null, 1, 5, 0, 50.0, "7e3fcb0f-11cd-4945-8d9a-84b6c717f94a" }
                });

            migrationBuilder.InsertData(
                table: "Hospitalization",
                columns: new[] { "HospitalizationId", "AdmissionDate", "DischargeDate", "KennelId", "PetId", "TotalCost", "VetId" },
                values: new object[,]
                {
                    { 1, new DateOnly(2023, 5, 1), new DateOnly(2023, 5, 5), 1, 1, 125.0, "7e3fcb0f-11cd-4945-8d9a-84b6c717f94a" },
                    { 2, new DateOnly(2023, 6, 10), new DateOnly(2023, 6, 12), 2, 2, 120.0, "7e3fcb0f-11cd-4945-8d9a-84b6c717f94a" },
                    { 3, new DateOnly(2023, 7, 15), new DateOnly(2023, 7, 18), 3, 3, 60.0, "4f0f8727-ddeb-40b5-92d4-22200c4dbb3d" },
                    { 4, new DateOnly(2023, 8, 1), new DateOnly(2023, 8, 3), 4, 4, 90.0, "4f0f8727-ddeb-40b5-92d4-22200c4dbb3d" },
                    { 5, new DateOnly(2023, 9, 10), new DateOnly(2023, 9, 12), 5, 5, 105.0, "4f0f8727-ddeb-40b5-92d4-22200c4dbb3d" }
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
                    { 1, 1, 1 },
                    { 2, 1, 2 },
                    { 3, 1, 3 },
                    { 4, 1, 4 },
                    { 5, 1, 5 }
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
                columns: new[] { "PetHealthTrackId", "Date", "Description", "HospitalizationId", "Status" },
                values: new object[,]
                {
                    { 1, new DateOnly(2023, 5, 3), "Recovering from surgery", 1, 0 },
                    { 2, new DateOnly(2023, 6, 11), "Monitoring for dehydration", 2, 1 },
                    { 3, new DateOnly(2023, 7, 16), "Treating respiratory infection", 3, 2 },
                    { 4, new DateOnly(2023, 8, 2), "Observing swim bladder disorder", 4, 0 },
                    { 5, new DateOnly(2023, 9, 11), "Managing gastrointestinal stasis", 5, 1 }
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
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

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
                name: "IX_Transaction_AppointmentId",
                table: "Transaction",
                column: "AppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_CustomerId",
                table: "Transaction",
                column: "CustomerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppointmentDetail");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "PetHealthTrack");

            migrationBuilder.DropTable(
                name: "PetVaccination");

            migrationBuilder.DropTable(
                name: "Transaction");

            migrationBuilder.DropTable(
                name: "Record");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Hospitalization");

            migrationBuilder.DropTable(
                name: "Vaccine");

            migrationBuilder.DropTable(
                name: "Appointment");

            migrationBuilder.DropTable(
                name: "Kennel");

            migrationBuilder.DropTable(
                name: "Pet");

            migrationBuilder.DropTable(
                name: "Service");

            migrationBuilder.DropTable(
                name: "Slot");

            migrationBuilder.DropTable(
                name: "Vet");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
