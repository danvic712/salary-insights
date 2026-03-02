using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalaryInsights.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateAuthProviderTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "auth_providers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    provider_type = table.Column<string>(type: "text", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_auth_providers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "auth_provider_settings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AuthProviderId = table.Column<Guid>(type: "uuid", nullable: false),
                    settings = table.Column<string>(type: "jsonb", nullable: false),
                    is_initialized = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_auth_provider_settings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_auth_provider_settings_auth_providers_AuthProviderId",
                        column: x => x.AuthProviderId,
                        principalTable: "auth_providers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_auth_provider_settings_AuthProviderId",
                table: "auth_provider_settings",
                column: "AuthProviderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_auth_providers_provider_type",
                table: "auth_providers",
                column: "provider_type");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "auth_provider_settings");

            migrationBuilder.DropTable(
                name: "auth_providers");
        }
    }
}
