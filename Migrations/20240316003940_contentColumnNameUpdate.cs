using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvestSense_API.Migrations
{
    /// <inheritdoc />
    public partial class contentColumnNameUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Conent",
                table: "Comment",
                newName: "Content");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Content",
                table: "Comment",
                newName: "Conent");
        }
    }
}
