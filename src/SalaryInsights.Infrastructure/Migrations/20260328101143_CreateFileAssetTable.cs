using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalaryInsights.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateFileAssetTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "file_assets",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    file_name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    file_size = table.Column<double>(type: "double precision", nullable: false),
                    content_type = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    file_hash = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    storage_type = table.Column<string>(type: "text", nullable: false),
                    file_path = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_file_assets", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_file_assets_file_hash",
                table: "file_assets",
                column: "file_hash");

            migrationBuilder.CreateIndex(
                name: "IX_file_assets_storage_type",
                table: "file_assets",
                column: "storage_type");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "file_assets");
        }
    }
}
