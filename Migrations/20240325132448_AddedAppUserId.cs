using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace InvestSense_API.Migrations
{
    /// <inheritdoc />
    public partial class AddedAppUserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2295bac1-10c6-4856-9656-a3a46518dd4f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6cbcacab-be5c-4ed8-98b4-f5e908054275");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Comment",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "35fc46de-d8d8-4003-976f-f52ca60e934d", null, "Admin", "ADMIN" },
                    { "88113180-9246-4ab1-bcdc-f06b4ae7e062", null, "User", "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comment_AppUserId",
                table: "Comment",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_AspNetUsers_AppUserId",
                table: "Comment",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_AspNetUsers_AppUserId",
                table: "Comment");

            migrationBuilder.DropIndex(
                name: "IX_Comment_AppUserId",
                table: "Comment");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "35fc46de-d8d8-4003-976f-f52ca60e934d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "88113180-9246-4ab1-bcdc-f06b4ae7e062");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Comment");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2295bac1-10c6-4856-9656-a3a46518dd4f", null, "User", "USER" },
                    { "6cbcacab-be5c-4ed8-98b4-f5e908054275", null, "Admin", "ADMIN" }
                });
        }
    }
}
