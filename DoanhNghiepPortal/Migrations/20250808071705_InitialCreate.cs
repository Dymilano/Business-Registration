using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DoanhNghiepPortal.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Businesses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BusinessCode = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    BusinessName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    EnglishName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ShortName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Province = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    District = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Ward = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Website = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    BusinessType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BusinessLine = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CharterCapital = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CapitalUnit = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    EstablishmentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LicenseDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LicenseNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Representative = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Position = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Businesses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BusinessLicenses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    BusinessName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    BusinessType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BusinessLine = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Province = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    District = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Ward = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Website = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Representative = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Position = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IdNumber = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    EstablishmentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    ApprovedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ApprovedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessLicenses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BusinessRegistrations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    BusinessName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    BusinessType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BusinessLine = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Province = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    District = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Ward = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Website = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CharterCapital = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CapitalUnit = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Representative = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Position = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IdNumber = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    EstablishmentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    ApprovedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ApprovedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessRegistrations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ConfirmPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Age = table.Column<int>(type: "int", nullable: true),
                    CurrentAddress = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    PermanentAddress = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IdCardNumber = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    Hometown = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TaxCode = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Businesses",
                columns: new[] { "Id", "Address", "BusinessCode", "BusinessLine", "BusinessName", "BusinessType", "CapitalUnit", "CharterCapital", "CreatedAt", "CreatedBy", "District", "Email", "EnglishName", "EstablishmentDate", "LicenseDate", "LicenseNumber", "Notes", "PhoneNumber", "Position", "Province", "Representative", "ShortName", "Status", "UpdatedAt", "UpdatedBy", "Ward", "Website" },
                values: new object[,]
                {
                    { 1, "123 Đường Nguyễn Huệ, Quận 1", "DN000000001", "Thương mại điện tử", "CÔNG TY TNHH ABC", "Công ty TNHH", "VND", 10000000000m, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin", "Quận 1", "info@abc.com.vn", "ABC COMPANY LIMITED", new DateTime(2020, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "GP123456", "", "028-12345678", "Giám đốc", "TP.HCM", "Nguyễn Văn A", "ABC", "Đang hoạt động", null, "", "Phường Bến Nghé", "www.abc.com.vn" },
                    { 2, "456 Đường Lê Lợi, Quận 3", "DN000000002", "Công nghệ thông tin", "CÔNG TY CỔ PHẦN XYZ", "Công ty cổ phần", "VND", 50000000000m, new DateTime(2022, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin", "Quận 3", "contact@xyz.com.vn", "XYZ JOINT STOCK COMPANY", new DateTime(2019, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "GP654321", "", "028-87654321", "Chủ tịch HĐQT", "TP.HCM", "Trần Thị B", "XYZ", "Đang hoạt động", null, "", "Phường Bến Thành", "www.xyz.com.vn" },
                    { 3, "789 Đường Trần Hưng Đạo, Quận 5", "DN000000003", "Dịch vụ tài chính", "DOANH NGHIỆP TƯ NHÂN DEF", "Doanh nghiệp tư nhân", "VND", 20000000000m, new DateTime(2023, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin", "Quận 5", "info@def.com.vn", "DEF PRIVATE ENTERPRISE", new DateTime(2021, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 3, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "GP112233", "", "028-11223344", "Chủ doanh nghiệp", "TP.HCM", "Lê Văn C", "DEF", "Tạm ngừng hoạt động", null, "", "Phường 1", "www.def.com.vn" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Age", "ConfirmPassword", "CreatedAt", "CurrentAddress", "Email", "FullName", "Hometown", "IdCardNumber", "IsActive", "Password", "PermanentAddress", "PhoneNumber", "TaxCode", "Username" },
                values: new object[] { 1, 30, "", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "123 Đường ABC, Quận 1, TP.HCM", "admin@example.com", "Nguyễn Văn Admin", "TP.HCM", "123456789012", true, "123456", "456 Đường XYZ, Quận 3, TP.HCM", "0123456789", "1234567890", "admin" });

            migrationBuilder.CreateIndex(
                name: "IX_Businesses_BusinessCode",
                table: "Businesses",
                column: "BusinessCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Businesses_BusinessName",
                table: "Businesses",
                column: "BusinessName");

            migrationBuilder.CreateIndex(
                name: "IX_Businesses_Province",
                table: "Businesses",
                column: "Province");

            migrationBuilder.CreateIndex(
                name: "IX_Businesses_Status",
                table: "Businesses",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessLicenses_CreatedAt",
                table: "BusinessLicenses",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessLicenses_Status",
                table: "BusinessLicenses",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessRegistrations_CreatedAt",
                table: "BusinessRegistrations",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessRegistrations_Status",
                table: "BusinessRegistrations",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Businesses");

            migrationBuilder.DropTable(
                name: "BusinessLicenses");

            migrationBuilder.DropTable(
                name: "BusinessRegistrations");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
