using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FitCookieAI_Data.Migrations
{
    /// <inheritdoc />
    public partial class GeneratedPlanTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GeneratedPlans",
                schema: "FitCookieAI",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    Plan = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneratedPlans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GeneratedPlans_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "FitCookieAI",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                schema: "FitCookieAI",
                table: "Admins",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DOB", "Password" },
                values: new object[] { new DateTime(2023, 8, 31, 23, 41, 17, 165, DateTimeKind.Local).AddTicks(9412), "qGl8FHhjwJm/0tYovT/PIF73XvOO/RcGwA3QWoSClK2rI5Ec8qtnbX7b8opGUxSC" });

            migrationBuilder.CreateIndex(
                name: "IX_GeneratedPlans_UserId",
                schema: "FitCookieAI",
                table: "GeneratedPlans",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GeneratedPlans",
                schema: "FitCookieAI");

            migrationBuilder.UpdateData(
                schema: "FitCookieAI",
                table: "Admins",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DOB", "Password" },
                values: new object[] { new DateTime(2023, 8, 12, 18, 59, 9, 302, DateTimeKind.Local).AddTicks(7361), "yHrTdPKDGQ/fLDbSfFSvTkLblZpBhk5i/2/dWi8puANwXTaPC4y9hiqOcUtYyl/z" });
        }
    }
}
