using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace InvestSense_API.Migrations
{
    /// <inheritdoc />
    public partial class SeededRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "43a0a6a1-33b8-49b8-9b83-6c7a0e65007d", null, "User", "USER" },
                    { "9157c2d8-7b8d-4604-900c-9b82243c90ae", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "43a0a6a1-33b8-49b8-9b83-6c7a0e65007d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9157c2d8-7b8d-4604-900c-9b82243c90ae");
        }
    }
}
