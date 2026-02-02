using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistance.Migrations
{
    /// <inheritdoc />
    public partial class mig_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PropertyAds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    RoomCount = table.Column<int>(type: "int", nullable: false),
                    Area = table.Column<double>(type: "float(18)", precision: 18, scale: 2, nullable: false),
                    Price = table.Column<double>(type: "float(18)", precision: 18, scale: 2, nullable: false),
                    IsExtract = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsMortgage = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    OfferType = table.Column<int>(type: "int", nullable: false),
                    PropertyCategoryId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyAds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PropertyMedias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PropertyAdId = table.Column<int>(type: "int", nullable: false),
                    MediaName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Order = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MediaUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyMedias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PropertyMedias_PropertyAds_PropertyAdId",
                        column: x => x.PropertyAdId,
                        principalTable: "PropertyAds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PropertyAds_CreatedAt",
                table: "PropertyAds",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyAds_OfferType",
                table: "PropertyAds",
                column: "OfferType");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyAds_Price",
                table: "PropertyAds",
                column: "Price");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyAds_PropertyCategoryId",
                table: "PropertyAds",
                column: "PropertyCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyMedias_PropertyAdId",
                table: "PropertyMedias",
                column: "PropertyAdId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyMedias_PropertyAdId_Order",
                table: "PropertyMedias",
                columns: new[] { "PropertyAdId", "Order" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PropertyMedias");

            migrationBuilder.DropTable(
                name: "PropertyAds");
        }
    }
}
