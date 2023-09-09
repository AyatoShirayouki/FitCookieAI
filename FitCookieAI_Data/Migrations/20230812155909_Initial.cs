using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FitCookieAI_Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "FitCookieAI");

            migrationBuilder.CreateTable(
                name: "AdminStatuses",
                schema: "FitCookieAI",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentPlans",
                schema: "FitCookieAI",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Description = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    PricePerMonth = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentPlans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "FitCookieAI",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DOJ = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    PhoneNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    Password = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    FirstName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    DOB = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Gender = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Admins",
                schema: "FitCookieAI",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StatusId = table.Column<int>(type: "integer", nullable: false),
                    ProfilePhotoName = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    Password = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    FirstName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    DOB = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Gender = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Admins_AdminStatuses_StatusId",
                        column: x => x.StatusId,
                        principalSchema: "FitCookieAI",
                        principalTable: "AdminStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PaymentPlanFeatures",
                schema: "FitCookieAI",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    PaymentPlanId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentPlanFeatures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentPlanFeatures_PaymentPlans_PaymentPlanId",
                        column: x => x.PaymentPlanId,
                        principalSchema: "FitCookieAI",
                        principalTable: "PaymentPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PasswordRecoveryTokens",
                schema: "FitCookieAI",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    Start = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    End = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PasswordRecoveryTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PasswordRecoveryTokens_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "FitCookieAI",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PaymentPlansToUsers",
                schema: "FitCookieAI",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PaymentPlanId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentPlansToUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentPlansToUsers_PaymentPlans_PaymentPlanId",
                        column: x => x.PaymentPlanId,
                        principalSchema: "FitCookieAI",
                        principalTable: "PaymentPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PaymentPlansToUsers_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "FitCookieAI",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                schema: "FitCookieAI",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Amount = table.Column<long>(type: "bigint", nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Currency = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    Source = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "FitCookieAI",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RefreshUserTokens",
                schema: "FitCookieAI",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    Token = table.Column<string>(type: "text", nullable: true),
                    JwtId = table.Column<string>(type: "text", nullable: true),
                    IsUsed = table.Column<bool>(type: "boolean", nullable: false),
                    IsRevorked = table.Column<bool>(type: "boolean", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshUserTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshUserTokens_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "FitCookieAI",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RefreshAdminTokens",
                schema: "FitCookieAI",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AdminId = table.Column<int>(type: "integer", nullable: false),
                    Token = table.Column<string>(type: "text", nullable: true),
                    JwtId = table.Column<string>(type: "text", nullable: true),
                    IsUsed = table.Column<bool>(type: "boolean", nullable: false),
                    IsRevorked = table.Column<bool>(type: "boolean", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshAdminTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshAdminTokens_Admins_AdminId",
                        column: x => x.AdminId,
                        principalSchema: "FitCookieAI",
                        principalTable: "Admins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "FitCookieAI",
                table: "AdminStatuses",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Developer" },
                    { 2, "Administrator" },
                    { 3, "Unverified" }
                });

            migrationBuilder.InsertData(
                schema: "FitCookieAI",
                table: "Admins",
                columns: new[] { "Id", "DOB", "Email", "FirstName", "Gender", "LastName", "Password", "ProfilePhotoName", "StatusId" },
                values: new object[] { 1, new DateTime(2023, 8, 12, 18, 59, 9, 302, DateTimeKind.Local).AddTicks(7361), "admin@mail.com", "Admin", "Male", "User", "yHrTdPKDGQ/fLDbSfFSvTkLblZpBhk5i/2/dWi8puANwXTaPC4y9hiqOcUtYyl/z", "", 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Admins_StatusId",
                schema: "FitCookieAI",
                table: "Admins",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_PasswordRecoveryTokens_UserId",
                schema: "FitCookieAI",
                table: "PasswordRecoveryTokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentPlanFeatures_PaymentPlanId",
                schema: "FitCookieAI",
                table: "PaymentPlanFeatures",
                column: "PaymentPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentPlansToUsers_PaymentPlanId",
                schema: "FitCookieAI",
                table: "PaymentPlansToUsers",
                column: "PaymentPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentPlansToUsers_UserId",
                schema: "FitCookieAI",
                table: "PaymentPlansToUsers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_UserId",
                schema: "FitCookieAI",
                table: "Payments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshAdminTokens_AdminId",
                schema: "FitCookieAI",
                table: "RefreshAdminTokens",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshUserTokens_UserId",
                schema: "FitCookieAI",
                table: "RefreshUserTokens",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PasswordRecoveryTokens",
                schema: "FitCookieAI");

            migrationBuilder.DropTable(
                name: "PaymentPlanFeatures",
                schema: "FitCookieAI");

            migrationBuilder.DropTable(
                name: "PaymentPlansToUsers",
                schema: "FitCookieAI");

            migrationBuilder.DropTable(
                name: "Payments",
                schema: "FitCookieAI");

            migrationBuilder.DropTable(
                name: "RefreshAdminTokens",
                schema: "FitCookieAI");

            migrationBuilder.DropTable(
                name: "RefreshUserTokens",
                schema: "FitCookieAI");

            migrationBuilder.DropTable(
                name: "PaymentPlans",
                schema: "FitCookieAI");

            migrationBuilder.DropTable(
                name: "Admins",
                schema: "FitCookieAI");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "FitCookieAI");

            migrationBuilder.DropTable(
                name: "AdminStatuses",
                schema: "FitCookieAI");
        }
    }
}
