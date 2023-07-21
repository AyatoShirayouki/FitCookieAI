using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitCookieAI_Data.Migrations
{
    /// <inheritdoc />
    public partial class PasswordRecoveryToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PasswordRecoveryTokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Start = table.Column<DateTime>(type: "datetime2", nullable: false),
                    End = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PasswordRecoveryTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PasswordRecoveryTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DOB", "Password" },
                values: new object[] { new DateTime(2023, 7, 19, 1, 11, 25, 67, DateTimeKind.Local).AddTicks(8756), "Q/ZgR25RKAN9RIbYMcIgG6YqAdeF+KG61cXBJBVDyd/EBnTpd9vb4ueouGYEO/WU" });

            migrationBuilder.CreateIndex(
                name: "IX_PasswordRecoveryTokens_UserId",
                table: "PasswordRecoveryTokens",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PasswordRecoveryTokens");

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DOB", "Password" },
                values: new object[] { new DateTime(2023, 7, 12, 22, 25, 48, 775, DateTimeKind.Local).AddTicks(7728), "+bhktk3BS/YhlZo50mtKhk525PiG8t7c7V/Cef+L+0zgOo3wLQLMyphJjvCelNOy" });
        }
    }
}
