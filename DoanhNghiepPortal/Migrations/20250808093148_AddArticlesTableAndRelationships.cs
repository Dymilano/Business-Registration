using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoanhNghiepPortal.Migrations
{
    /// <inheritdoc />
    public partial class AddArticlesTableAndRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Summary = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Category = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Author = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    IsFeatured = table.Column<bool>(type: "bit", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    ViewCount = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PublishedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BusinessRegistrations_UserId",
                table: "BusinessRegistrations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessLicenses_UserId",
                table: "BusinessLicenses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_Category",
                table: "Articles",
                column: "Category");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_CreatedAt",
                table: "Articles",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_IsFeatured",
                table: "Articles",
                column: "IsFeatured");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_Status",
                table: "Articles",
                column: "Status");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessLicenses_Users_UserId",
                table: "BusinessLicenses",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessRegistrations_Users_UserId",
                table: "BusinessRegistrations",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusinessLicenses_Users_UserId",
                table: "BusinessLicenses");

            migrationBuilder.DropForeignKey(
                name: "FK_BusinessRegistrations_Users_UserId",
                table: "BusinessRegistrations");

            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DropIndex(
                name: "IX_BusinessRegistrations_UserId",
                table: "BusinessRegistrations");

            migrationBuilder.DropIndex(
                name: "IX_BusinessLicenses_UserId",
                table: "BusinessLicenses");
        }
    }
}
