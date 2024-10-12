using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Codeteasers.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddingnormalizedUsernametouser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NormalizedUsername",
                table: "Users",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Users_NormalizedUsername",
                table: "Users",
                column: "NormalizedUsername",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Problems_NormalizedTitle",
                table: "Problems",
                column: "NormalizedTitle",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_NormalizedUsername",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Problems_NormalizedTitle",
                table: "Problems");

            migrationBuilder.DropColumn(
                name: "NormalizedUsername",
                table: "Users");
        }
    }
}
