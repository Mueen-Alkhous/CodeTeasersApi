using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Codeteasers.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Addingnormalizednametoproblem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NormalizedTitle",
                table: "Problems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NormalizedTitle",
                table: "Problems");
        }
    }
}
